namespace FacilitiesCoordinator.API.Features.FacilityGroup.GetById;

public sealed record GetByIdFacilityGroupResponse(
    int Id,
    string Name,
    DateTime CreatedAt
);