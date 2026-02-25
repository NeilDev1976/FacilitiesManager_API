namespace FacilitiesCoordinator.API.Features.Users.GetAll;

public sealed record GetAllUsersResponse(
    int Id,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    bool IsActive,
    IReadOnlyList<string> Roles
);