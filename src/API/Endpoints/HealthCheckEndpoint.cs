namespace FacilitiesCoordinator.API.Endpoints;

public static class HealthCheckEndpoint
{
    public static IEndpointRouteBuilder MapHealthCheck(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow }))
           .WithName("HealthCheck")
           .WithTags("Monitoring");

        return app;
    }
}
