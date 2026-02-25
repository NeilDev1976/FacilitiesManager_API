namespace FacilitiesCoordinator.API.Features.Users.Update;

using FacilitiesCoordinator.API.Common;
public static class UpdateUserEndpoint
{
    public static IEndpointRouteBuilder MapUpdateUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPatch("/users/{id:int}", async (
            int id,
            UpdateUserRequest request,
            UpdateUserHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, request, ct);

            return result.Status switch
            {
                UpdateUserStatus.NotFound => Results.NotFound(),
                UpdateUserStatus.NoChanges => Results.NoContent(),
                UpdateUserStatus.Updated => Results.Ok(result.Response),
                UpdateUserStatus.UsernameAlreadyExists => Results.Conflict(new { Message = "Username already exists" }),
                UpdateUserStatus.EmailAlreadyExists => Results.Conflict(new { Message = "Email already exists" }),
                _ => Results.Problem(
                    detail: $"Unhandled status: {result.Status}",
                    statusCode: StatusCodes.Status500InternalServerError)
            };
        })
        .Produces<UpdateUserResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .AddEndpointFilter<ValidationFilter<UpdateUserRequest>>()
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}