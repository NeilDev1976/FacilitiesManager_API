namespace FacilitiesCoordinator.API.Features.Users.Update;

using FluentValidation;

public sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(u => u.Username)
            .MaximumLength(50)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Username cannot be blank.")
            .When(u => u.Username is not null);

        RuleFor(u => u.Email)
            .MaximumLength(255)
            .EmailAddress()
            .When(u => u.Email is not null);

        RuleFor(u => u.FirstName)
            .MaximumLength(100)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("First name cannot be blank.")
            .When(u => u.FirstName is not null);

        RuleFor(u => u.LastName)
            .MaximumLength(100)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Last name cannot be blank.")
            .When(u => u.LastName is not null);
    }
}