namespace FacilitiesCoordinator.API.Features.Users.Create;

using FluentValidation;

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .MaximumLength(50)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Username cannot be blank.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .MaximumLength(255)
            .EmailAddress();

        RuleFor(u => u.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("First name cannot be blank.");

        RuleFor(u => u.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Last name cannot be blank.");

        RuleFor(u => u.Roles)
            .NotNull()
            .NotEmpty();

        RuleForEach(u => u.Roles)
            .NotEmpty()
            .Must(r => !string.IsNullOrWhiteSpace(r))
            .WithMessage("Role cannot be blank.");
    }
}