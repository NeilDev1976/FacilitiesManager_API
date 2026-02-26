namespace FacilitiesCoordinator.API.Features.Users.UpdateRole;

using FluentValidation;

public sealed class UpdateUserRoleRequestValidator : AbstractValidator<UpdateUserRoleRequest>
{
    public UpdateUserRoleRequestValidator()
    {
        RuleFor(u => u.Roles)
            .NotNull()
            .NotEmpty();

        RuleForEach(u => u.Roles)
            .Must(r => !string.IsNullOrWhiteSpace(r))
            .WithMessage("Role cannot be blank.");
    }
}