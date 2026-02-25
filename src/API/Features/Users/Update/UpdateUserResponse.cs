namespace FacilitiesCoordinator.API.Features.Users.Update;

public enum UpdateUserStatus
{
    Updated,
    NotFound,
    UsernameAlreadyExists,
    EmailAlreadyExists,
    NoChanges
}

public sealed record UpdateUserResult(
    UpdateUserStatus Status,
    UpdateUserResponse? Response = null
);

public sealed record UpdateUserResponse(
    int Id,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    bool IsActive
);