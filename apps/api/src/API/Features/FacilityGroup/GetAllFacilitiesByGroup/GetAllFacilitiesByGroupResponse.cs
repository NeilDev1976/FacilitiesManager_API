namespace FacilitiesCoordinator.API.Features.FacilityGroup.GetAllFacilitiesByGroup;

public enum GetAllFacilitiesByGroupStatus
{
    Success,
    NotFound
};

public sealed record GetAllFacilitiesByGroupResult(
    GetAllFacilitiesByGroupStatus Status,
    IReadOnlyList<GetAllFacilitiesByGroupResponse>? Facilities = null
);

public sealed record GetAllFacilitiesByGroupResponse(
    string Code,
    string Name,
    string CurrentStatus
);