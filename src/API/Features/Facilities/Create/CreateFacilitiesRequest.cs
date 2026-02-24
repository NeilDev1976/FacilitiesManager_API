namespace FacilitiesCoordinator.API.Features.Facilities.Create;

public sealed record CreateFacilityRequest(string Code, string Name, int GroupId);