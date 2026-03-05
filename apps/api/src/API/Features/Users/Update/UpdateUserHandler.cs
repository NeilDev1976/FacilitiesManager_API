using FacilitiesCoordinator.Domain.Entities;
using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FacilitiesCoordinator.API.Features.Users.Update;

public sealed class UpdateUserHandler
{
    private readonly AppDbContext _db;

    private readonly ILogger<UpdateUserHandler> _logger;

    public UpdateUserHandler(AppDbContext db, ILogger<UpdateUserHandler> logger) => (_db, _logger) = (db, logger);

    public async Task<UpdateUserResult> HandleAsync(int id, UpdateUserRequest request, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
        if (user is null) return new(UpdateUserStatus.NotFound);

        var changed = false;

        // Username
        if (request.Username is not null)
        {
            var trimmed = request.Username.Trim();

            if (!string.Equals(user.Username, trimmed, StringComparison.Ordinal))
            {
                var exists = await _db.Users.AnyAsync(u => u.Id != id && u.Username == trimmed, ct);
                if (exists)
                {
                    _logger.LogWarning("UpdateUser failed: Username {Username} already exists", trimmed);
                    return new(UpdateUserStatus.UsernameAlreadyExists);
                }

                user.Username = trimmed;
                changed = true;
            }
        }

        // Email
        if (request.Email is not null)
        {
            var normalized = request.Email.Trim().ToLowerInvariant();

            if (!string.Equals(user.Email, normalized, StringComparison.Ordinal))
            {
                var exists = await _db.Users.AnyAsync(u => u.Id != id && u.Email == normalized, ct);
                if (exists)
                {
                    _logger.LogWarning("UpdateUser failed: Email {Email} already exists", normalized);
                    return new(UpdateUserStatus.EmailAlreadyExists);
                }

                user.Email = normalized;
                changed = true;
            }
        }

        // FirstName
        if (request.FirstName is not null)
        {
            var trimmed = request.FirstName.Trim();
            if (!string.Equals(user.FirstName, trimmed, StringComparison.Ordinal))
            {
                user.FirstName = trimmed;
                changed = true;
            }
        }

        // LastName
        if (request.LastName is not null)
        {
            var trimmed = request.LastName.Trim();
            if (!string.Equals(user.LastName, trimmed, StringComparison.Ordinal))
            {
                user.LastName = trimmed;
                changed = true;
            }
        }

        // IsActive
        if (request.IsActive.HasValue && user.IsActive != request.IsActive.Value)
        {
            user.IsActive = request.IsActive.Value;
            changed = true;
        }

        if (!changed) return new(UpdateUserStatus.NoChanges);

        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("User {UserId} updated", user.Id);

        return new(UpdateUserStatus.Updated, new UpdateUserResponse(
            user.Id,
            user.Username,
            user.Email,
            user.FirstName,
            user.LastName,
            user.IsActive
        ));
    }
}