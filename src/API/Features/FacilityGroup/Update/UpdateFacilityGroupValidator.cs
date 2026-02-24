namespace FacilitiesCoordinator.API.Features.FacilityGroup.Update;

using FluentValidation;

public sealed class UpdateFacilityGroupRequestValidator : AbstractValidator<UpdateFacilityGroupRequest>
{
    public UpdateFacilityGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(255);

        RuleFor(x => x)
            .Must(x => x.Name is not null)
            .WithMessage("At least one field must be provided.");
    }
}