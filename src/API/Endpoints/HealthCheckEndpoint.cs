namespace FacilitiesCoordinator.API.Endpoints;

public static class HealthCheckEndpoint
{
    public static IEndpointRouteBuilder MapHealthCheck(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", GetHealth)
           .WithName("HealthCheck")
           .WithTags("Monitoring");

        return app;
    }

    /// <summary>
    /// Returns the health status of the API.
    /// </summary>
    /// <returns>A JSON object with status and UTC timestamp.</returns>
    private static IResult GetHealth()
    {
        return Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }
}
