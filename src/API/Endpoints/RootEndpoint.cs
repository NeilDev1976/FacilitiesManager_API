namespace FacilitiesCoordinator.API.Endpoints;


public static class RootEndpoint
{
    public static IEndpointRouteBuilder MapRootEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => Results.Ok(new
        {
            service = "FacilitiesCoordinator API",
            version = "v1",
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
        }))
        .WithName("Root")
        .WithTags("System");

        return app;
    }
}
