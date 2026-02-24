namespace FacilitiesCoordinator.API.Features.FacilityGroup.GetById;

public static class GetByIdFacilityGroupEndpoint
{
    public static IEndpointRouteBuilder MapGetByIdFacilityGroupEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/facility-group/{id}", async (
            int id, 
            GetByIdFacilityGroupHandler handler, 
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, ct);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithName("GetByIdFacilityGroup")
        .WithTags("FacilityGroups")
        .Produces<GetByIdFacilityGroupResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem();
    return app;
    }
}