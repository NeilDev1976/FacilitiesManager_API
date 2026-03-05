namespace FacilitiesCoordinator.API.Features.Facilities.Create;

using FacilitiesCoordinator.API.Common;
public static class CreateFacilityEndpoint
{
    public static IEndpointRouteBuilder MapCreateFacilityEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/facilities", async (
                CreateFacilityRequest request,
                CreateFacilityHandler handler,
                CancellationToken ct) =>
            {
                var response = await handler.HandleAsync(request, ct);
                return Results.Created($"/facilities/{response.Id}", response);
            })
            .WithName("CreateFacility")
            .WithTags("Facilities")
            .AddEndpointFilter<ValidationFilter<CreateFacilityRequest>>()
            .Produces<CreateFacilityResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        return app;
    }
}

