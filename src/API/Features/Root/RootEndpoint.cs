namespace FacilitiesCoordinator.Features.Root;


public static class RootEndpoint
{
    public static IEndpointRouteBuilder MapRootEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (HttpContext context) => GetRoot(context))
        .RequireAuthorization("Admin")
        .WithName("Root")
        .WithTags("System");

        return app;
    }

    /// <summary>
    /// Root endpoint providing basic information about the API.
    /// </summary>
    /// <returns>A JSON object with version and environment information.</returns>
    private static IResult GetRoot(HttpContext context)
    {
        var name = context.User.Identity?.Name;
        var roles = context.User.Claims.Where(c => c.Type == context.User.Identities.First().RoleClaimType)
                               .Select(c => c.Value)
                               .ToArray();
        
        
        
        return Results.Ok(new {
            service = "FacilitiesCoordinator API",
            version = "v1",
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
            user = name,
            roles = roles,
            email = context.User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value
            
        });
    }
}
