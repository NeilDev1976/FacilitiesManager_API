namespace FacilitiesCoordinator.API.Features.Users.UpdateRole;

public enum UpdateUserRoleStatus
{
    Updated,
    NotFound,
    NoChanges,
    InvalidRoles
}

public sealed record UpdateUserRoleResult(
    UpdateUserRoleStatus Status,
    UpdateUserRoleResponse? Response = null,
    IReadOnlyList<string>? InvalidRoles = null
);

public sealed record UpdateUserRoleResponse(
    IReadOnlyList<string> Roles
);