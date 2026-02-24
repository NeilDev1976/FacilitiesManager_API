namespace FacilitiesCoordinator.API.Features.Facilities.Create;

public sealed record CreateFacilityResponse(
    int Id,
    string Code,
    string Name,
    string CurrentStatus,
    DateTime CreatedAt,
    int GroupId
);