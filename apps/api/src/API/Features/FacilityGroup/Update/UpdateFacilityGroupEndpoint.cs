namespace FacilitiesCoordinator.API.Features.FacilityGroup.Update;
public static class UpdateFacilityGroupEndpoint
{
    public static IEndpointRouteBuilder MapUpdateFacilityGroupEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPatch("/facility-group/{id}", async (
            int id,
            UpdateFacilityGroupRequest request,
            UpdateFacilityGroupHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, request, ct);

            return result.Status switch
            {
                UpdateFacilityGroupStatus.NotFound => Results.NotFound(),
                UpdateFacilityGroupStatus.NoChanges => Results.NoContent(),
                UpdateFacilityGroupStatus.Updated => Results.Ok(result.Response),
                _ => Results.StatusCode(500)
            };
        })
        .Produces<UpdateFacilityGroupResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();

        return app;
    }
}