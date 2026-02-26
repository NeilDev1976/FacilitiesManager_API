using FacilitiesCoordinator.Domain.Entities;
using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FacilitiesCoordinator.API.Features.Users.UpdateRole;

public sealed class UpdateUserRoleHandler
{
    private readonly AppDbContext _db;

    private readonly ILogger<UpdateUserRoleHandler> _logger;

    public UpdateUserRoleHandler(AppDbContext db, ILogger<UpdateUserRoleHandler> logger) => (_db, _logger) = (db, logger);

    public async Task<UpdateUserRoleResult> HandleAsync(int id, UpdateUserRoleRequest request, CancellationToken ct)
    {
        var user = await _db.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.Id == id, ct);

        if (user is null) return new(UpdateUserRoleStatus.NotFound);

        var changed = false;

        var requested = request.Roles.Select(r => r.Trim().ToUpperInvariant()).Distinct().ToList();

        var roles = await _db.Roles
            .Where(r => requested.Contains(r.NameNormalized))
            .ToListAsync(ct);

        var invalid = requested.Except(roles.Select(r => r.NameNormalized)).ToList();

        if (invalid.Count > 0)
        {
            _logger.LogWarning("UpdateUserRole failed: Invalid roles {Roles} requested for user {UserId}", invalid, user.Id);
            return new(UpdateUserRoleStatus.InvalidRoles, InvalidRoles: invalid);
        }

        var currentRoleIds = user.UserRoles.Select(ur => ur.RoleId).ToHashSet();
        var requestedRoleIds = roles.Select(r => r.Id).ToHashSet();

        var rolesToAdd = requestedRoleIds.Except(currentRoleIds);
        var rolesToRemove = currentRoleIds.Except(requestedRoleIds);

        foreach (var roleId in rolesToAdd)
        {
            user.UserRoles.Add(new UserRole { RoleId = roleId, AssignedAt = DateTime.UtcNow });
            changed = true;
        }

        foreach (var ur in user.UserRoles.Where(ur => rolesToRemove.Contains(ur.RoleId)).ToList())
        {
            user.UserRoles.Remove(ur);
            changed = true;
        }

        if (!changed) return new(UpdateUserRoleStatus.NoChanges);

        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
        _logger.LogInformation("User {UserId} roles updated", user.Id);

        return new(
            UpdateUserRoleStatus.Updated,
            new UpdateUserRoleResponse(roles.Select(r => r.Name).ToList())
        );
    }
}