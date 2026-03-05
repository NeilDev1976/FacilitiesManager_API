namespace FacilitiesCoordinator.API.Features.FacilityGroup.Create;

public sealed record CreateFacilityGroupResponse(
    int Id,
    string Name,
    DateTime CreatedAt
);