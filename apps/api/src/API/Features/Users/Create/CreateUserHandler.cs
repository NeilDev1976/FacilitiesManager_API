using FacilitiesCoordinator.Domain.Entities;
using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FacilitiesCoordinator.API.Features.Users.Create;

public sealed class CreateUserHandler
{
    private readonly AppDbContext _db;

    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(AppDbContext db, ILogger<CreateUserHandler> logger) => (_db, _logger) = (db, logger);

    public async Task<CreateUserResult> HandleAsync(CreateUserRequest request, CancellationToken ct)
    {
        var username = request.Username.Trim();
        var email = request.Email.Trim().ToLowerInvariant();

        if (await _db.Users.AnyAsync(u => u.Username == username, ct))
        {
            _logger.LogWarning("CreateUser failed: Username {Username} already exists", username);
            return new(CreateUserStatus.UsernameAlreadyExists);
        }

        if (await _db.Users.AnyAsync(u => u.Email == email, ct))
        {
            _logger.LogWarning("CreateUser failed: Email {Email} already exists", email);
            return new(CreateUserStatus.EmailAlreadyExists);
        }

        // Normalize roles to uppercase keys
        var roleKeys = request.Roles
            .Select(r => r.Trim().ToUpperInvariant())
            .Distinct()
            .ToList();

        // Optional: default role if none provided
        if (roleKeys.Count == 0)
            roleKeys.Add("USER");

        // Load roles from DB
        var roles = await _db.Roles
            .Where(r => roleKeys.Contains(r.NameNormalized))
            .ToListAsync(ct);

        var foundKeys = roles.Select(r => r.NameNormalized).ToHashSet();
        var invalid = roleKeys.Where(k => !foundKeys.Contains(k)).ToList();

        if (invalid.Count > 0)
        {
            _logger.LogWarning("CreateUser failed: Invalid roles {Roles} requested for {Username}", invalid, username);
            return new(CreateUserStatus.InvalidRoles, InvalidRoles: invalid);
        }

        var user = new User
        {
            Username = username,
            Email = email,
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        foreach (var role in roles)
        {
            user.UserRoles.Add(new UserRole
            {
                RoleId = role.Id,
                AssignedAt = DateTime.UtcNow
            });
        }

        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("User {Username} created with ID {UserId}", username, user.Id);

        return new(CreateUserStatus.Created, new CreateUserResponse(
            user.Id,
            user.Username,
            user.Email,
            user.FirstName,
            user.LastName,
            user.IsActive,
            roles.OrderBy(r => r.NameNormalized).Select(r => r.Name).ToList()
        ));
    }
}