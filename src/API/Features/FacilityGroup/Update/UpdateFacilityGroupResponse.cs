namespace FacilitiesCoordinator.API.Features.FacilityGroup.Update;

public enum UpdateFacilityGroupStatus
{
    Updated,
    NotFound,
    NoChanges
}

public sealed record UpdateFacilityGroupResult(
    UpdateFacilityGroupStatus Status,
    UpdateFacilityGroupResponse? Response = null
);

public sealed record UpdateFacilityGroupResponse(
    int Id,
    string Name
);