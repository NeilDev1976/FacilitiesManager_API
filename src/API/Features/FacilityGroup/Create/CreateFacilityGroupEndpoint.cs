namespace FacilitiesCoordinator.API.Features.FacilityGroup.Create;

using FacilitiesCoordinator.API.Common;
public static class CreateFacilityEndpoint
{
    public static IEndpointRouteBuilder MapCreateFacilityGroupEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/facility-group", async (
                CreateFacilityGroupRequest request,
                CreateFacilityGroupHandler handler,
                CancellationToken ct) =>
            {
                var response = await handler.HandleAsync(request, ct);
                return Results.Created($"/facility-group/{response.Id}", response);
            })
            .WithName("CreateFacilityGroup")
            .WithTags("FacilityGroups")
            .AddEndpointFilter<ValidationFilter<CreateFacilityGroupRequest>>()
            .Produces<CreateFacilityGroupResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        return app;
    }
}

