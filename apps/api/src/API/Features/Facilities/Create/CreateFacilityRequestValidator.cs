namespace FacilitiesCoordinator.API.Features.Facilities.Create;

using FluentValidation;

public sealed class CreateFacilityRequestValidator : AbstractValidator<CreateFacilityRequest>
{
    public CreateFacilityRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(4);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}