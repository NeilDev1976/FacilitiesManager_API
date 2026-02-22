namespace FacilitiesCoordinator.API.Features.Facilities.Read;

public sealed record ReadFacilitiesResponse(
    string Code,
    string Name,
    string CurrentStatus
);