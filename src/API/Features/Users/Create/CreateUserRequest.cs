namespace FacilitiesCoordinator.API.Features.Users.Create;

public sealed record CreateUserRequest(
    string Username,
    string Email,
    string FirstName,
    string LastName,
    bool IsActive,
    IReadOnlyList<string> Roles);