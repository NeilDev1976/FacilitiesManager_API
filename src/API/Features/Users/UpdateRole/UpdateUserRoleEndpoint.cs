namespace FacilitiesCoordinator.API.Features.Users.UpdateRole;

using FacilitiesCoordinator.API.Common;
public static class UpdateUserRoleEndpoint
{
    public static IEndpointRouteBuilder MapUpdateUserRoleEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{id:int}/roles", async (
            int id,
            UpdateUserRoleRequest request,
            UpdateUserRoleHandler handler,
            CancellationToken ct) =>
        {
            var result = await handler.HandleAsync(id, request, ct);

            return result.Status switch
            {
                UpdateUserRoleStatus.NotFound => Results.NotFound(),
                UpdateUserRoleStatus.NoChanges => Results.NoContent(),
                UpdateUserRoleStatus.Updated => Results.Ok(result.Response),
                UpdateUserRoleStatus.InvalidRoles => Results.BadRequest(new { Message = "Invalid roles provided" }),
                _ => Results.Problem(
                    detail: $"Unhandled status: {result.Status}",
                    statusCode: StatusCodes.Status500InternalServerError)
            };
        })
        .Produces<UpdateUserRoleResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .AddEndpointFilter<ValidationFilter<UpdateUserRoleRequest>>()
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }
}