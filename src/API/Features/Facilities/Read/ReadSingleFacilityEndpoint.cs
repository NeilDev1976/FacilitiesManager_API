namespace FacilitiesCoordinator.API.Features.Facilities.Read;

using FacilitiesCoordinator.API.Common;

public static class ReadSingleFacilityEndpoint
{
    public static IEndpointRouteBuilder MapReadSingleFacilityEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/facilities/{code}", async (
            [AsParameters] ReadSingleFacilityRequest request, 
            ReadSingleFacilityHandler handler, 
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(request.Code, ct);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithName("ReadSingleFacility")
        .WithTags("Facilities")
        .AddEndpointFilter<ValidationFilter<ReadSingleFacilityRequest>>()
        .Produces<ReadFacilitiesResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem();
    return app;
    }
}