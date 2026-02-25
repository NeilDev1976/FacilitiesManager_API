namespace FacilitiesCoordinator.API.Features.Users.Create;

using FacilitiesCoordinator.API.Common;

public static class CreateUserEndpoint
{
    public static IEndpointRouteBuilder MapCreateUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (
                CreateUserRequest request,
                CreateUserHandler handler,
                CancellationToken ct) =>
            {
                var response = await handler.HandleAsync(request, ct);

                return response.Status switch
                {
                    CreateUserStatus.Created =>
                        Results.Created($"/users/{response.Response!.Id}", response.Response),
                    CreateUserStatus.UsernameAlreadyExists =>
                        Results.Conflict(new { Message = "Username already exists." }),
                    CreateUserStatus.EmailAlreadyExists =>
                        Results.Conflict(new { Message = "Email already exists." }),
                    CreateUserStatus.InvalidRoles =>
                        Results.BadRequest(new { Message = "One or more provided roles are invalid.", InvalidRoles = response.InvalidRoles }),
                    _ =>  Results.Problem(
                        detail: $"Unhandled status: {response.Status}",
                        statusCode: StatusCodes.Status500InternalServerError)
                };
                
            })
            .WithName("CreateUser")
            .WithTags("Users")
            .AddEndpointFilter<ValidationFilter<CreateUserRequest>>()
            .Produces<CreateUserResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem();

        return app;
    }
}

