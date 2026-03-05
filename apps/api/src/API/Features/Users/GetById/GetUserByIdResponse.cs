namespace FacilitiesCoordinator.API.Features.Users.GetById;

public sealed record GetUserByIdResponse(
    int Id,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    bool IsActive,
    IReadOnlyList<string> Roles
);