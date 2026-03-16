namespace FacilitiesCoordinator.API.Features.Facilities.Update;
public static class UpdateFacilityEndpoint
{
    public static IEndpointRouteBuilder MapUpdateFacilityEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPatch("/facilities/{id}", async (
            int id,
            UpdateFacilityRequest request,
            UpdateFacilityHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, request, ct);

            return result.Status switch
            {
                UpdateFacilityStatus.NotFound => Results.NotFound(),
                UpdateFacilityStatus.NoChanges => Results.NoContent(),
                UpdateFacilityStatus.Updated => Results.Ok(result.Response),
                _ => Results.StatusCode(500)
            };
        })
        .Produces<UpdateFacilityResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();

        return app;
    }
}