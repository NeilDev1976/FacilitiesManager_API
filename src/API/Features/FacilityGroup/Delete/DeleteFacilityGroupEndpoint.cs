namespace FacilitiesCoordinator.API.Features.FacilityGroup.Delete;

public static class DeleteFacilityGroupEndpoint
{
    public static IEndpointRouteBuilder MapDeleteFacilityGroupEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/facility-group/{id}", async (
            int id, 
            DeleteFacilityGroupHandler handler, 
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, ct);
            return result == false ? Results.NotFound() : Results.NoContent();
        })
        .WithName("DeleteFacilityGroup")
        .WithTags("FacilityGroups")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
    return app;
    }
}