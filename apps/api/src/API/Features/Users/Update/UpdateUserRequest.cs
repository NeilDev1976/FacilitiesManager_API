namespace FacilitiesCoordinator.API.Features.Users.Update;

public sealed record UpdateUserRequest(
    string? Username,
    string? Email,
    string? FirstName,
    string? LastName,
    bool? IsActive);