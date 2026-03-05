namespace FacilitiesCoordinator.API.Features.FacilityGroup.GetAll;
public static class GetAllFacilityGroupsEndpoint
{
    public static IEndpointRouteBuilder MapGetAllFacilityGroupsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/facility-group", async (
                GetAllFacilityGroupsHandler handler,
                CancellationToken ct) =>
            {
                var response = await handler.HandleAsync(ct);
                return Results.Ok(response);
            })
            .WithName("GetAllFacilityGroups")
            .WithTags("FacilityGroups")
            .Produces<GetAllFacilityGroupResponse>(StatusCodes.Status200OK);

        return app;
    }
}