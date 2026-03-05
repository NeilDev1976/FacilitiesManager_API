namespace FacilitiesCoordinator.API.Common;

using FluentValidation;

public sealed class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        // 1) Pull the request DTO (T) out of the endpoint arguments
        var model = context.Arguments.OfType<T>().FirstOrDefault();
        if (model is null)
            return await next(context); // nothing to validate

        // 2) Get the validator for T from DI
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is null)
            return await next(context); // no validator registered

        // 3) Use the request cancellation token
        var ct = context.HttpContext.RequestAborted;

        // 4) Validate
        var result = await validator.ValidateAsync(model, ct);

        // 5) If invalid, return a standard 400 validation response
        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            return Results.ValidationProblem(errors);
        }

        // 6) If valid, continue to the endpoint handler
        return await next(context);
    }
}