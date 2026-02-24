namespace FacilitiesCoordinator.API.Features.FacilityGroup.GetAll;

public sealed record GetAllFacilityGroupResponse(
    int Id,
    string Name,
    DateTime CreatedAt
);