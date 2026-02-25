namespace FacilitiesCoordinator.API.Features.Users.Create;

public enum CreateUserStatus
{
    Created,
    UsernameAlreadyExists,
    EmailAlreadyExists,
    InvalidRoles
}

public sealed record CreateUserResult(
    CreateUserStatus Status,
    CreateUserResponse? Response = null,
    IReadOnlyList<string>? InvalidRoles = null
);

public sealed record CreateUserResponse(
    int Id,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    bool IsActive,
    IReadOnlyList<string> Roles
);