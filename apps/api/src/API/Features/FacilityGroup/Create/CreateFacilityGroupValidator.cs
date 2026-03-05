namespace FacilitiesCoordinator.API.Features.FacilityGroup.Create;

using FluentValidation;

public sealed class CreateFacilityGroupRequestValidator : AbstractValidator<CreateFacilityGroupRequest>
{
    public CreateFacilityGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}