namespace FacilitiesCoordinator.API.Features.Users.UpdateRole;

public sealed record UpdateUserRoleRequest(
     IReadOnlyList<string> Roles);