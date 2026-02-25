namespace FacilitiesCoordinator.API.Features.FacilityGroup.GetAllFacilitiesByGroup;

public static class GetAllFacilitiesByGroupEndpoint
{
    public static IEndpointRouteBuilder MapGetAllFacilitiesByGroupEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/facility-group/{id:int}/facilities", async (
            int id, 
            GetAllFacilitiesByGroupHandler handler, 
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, ct);
            return result.Status == GetAllFacilitiesByGroupStatus.NotFound ? Results.NotFound() : Results.Ok(result.Facilities);
        })
        .WithName("GetAllFacilitiesByGroup")
        .WithTags("FacilityGroups")
        .Produces<IReadOnlyList<GetAllFacilitiesByGroupResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    return app;
    }
}