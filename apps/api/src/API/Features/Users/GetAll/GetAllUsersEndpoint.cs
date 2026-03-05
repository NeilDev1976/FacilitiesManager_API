namespace FacilitiesCoordinator.API.Features.Users.GetAll;
public static class GetAllUsersEndpoint
{
    public static IEndpointRouteBuilder MapGetAllUsersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (
                GetAllUsersHandler handler,
                CancellationToken ct) =>
            {
                var response = await handler.HandleAsync(ct);
                return Results.Ok(response);
            })
            .WithName("GetAllUsers")
            .WithTags("Users")
            .Produces<GetAllUsersResponse>(StatusCodes.Status200OK);

        return app;
    }
}