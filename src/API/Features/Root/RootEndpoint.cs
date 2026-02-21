namespace FacilitiesCoordinator.Features.Root;


public static class RootEndpoint
{
    public static IEndpointRouteBuilder MapRootEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetRoot)
        .WithName("Root")
        .WithTags("System");

        return app;
    }

    /// <summary>
    /// Root endpoint providing basic information about the API.
    /// </summary>
    /// <returns>A JSON object with version and environment information.</returns>
    private static IResult GetRoot()
    {
        return Results.Ok(new {
            service = "FacilitiesCoordinator API",
            version = "v1",
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
        });
    }
}
