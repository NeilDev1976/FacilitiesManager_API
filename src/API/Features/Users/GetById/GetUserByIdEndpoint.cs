namespace FacilitiesCoordinator.API.Features.Users.GetById;

using FacilitiesCoordinator.API.Common;

public static class GetUserByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetUserByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:int}", async (
            int id, 
            GetUserByIdHandler handler, 
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, ct);
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithName("GetUserById")
        .WithTags("Users")
        .Produces<GetUserByIdResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
    return app;
    }
}