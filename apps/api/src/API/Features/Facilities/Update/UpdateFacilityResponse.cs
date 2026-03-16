namespace FacilitiesCoordinator.API.Features.Facilities.Update;

public enum UpdateFacilityStatus
{
    Updated,
    NotFound,
    NoChanges
}

public sealed record UpdateFacilityResult(
    UpdateFacilityStatus Status,
    UpdateFacilityResponse? Response = null
);

public sealed record UpdateFacilityResponse(
    int Id,
    string Code,
    string Name,
    string CurrentStatus,
    DateTime UpdatedAt,
    int GroupId
);