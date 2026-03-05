namespace FacilitiesCoordinator.API.Features.Facilities.Read;
public static class ReadFacilitiesEndpoint
{
    public static IEndpointRouteBuilder MapReadFacilitiesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/facilities", async (
                ReadFacilitiesHandler handler,
                CancellationToken ct) =>
            {
                var response = await handler.HandleAsync(ct);
                return Results.Ok(response);
            })
            .WithName("ReadFacility")
            .WithTags("Facilities")
            .Produces<ReadFacilitiesResponse>(StatusCodes.Status200OK);

        return app;
    }
}