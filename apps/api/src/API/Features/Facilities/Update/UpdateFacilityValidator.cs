namespace FacilitiesCoordinator.API.Features.Facilities.Update;

using FluentValidation;

public sealed class UpdateFacilityRequestValidator : AbstractValidator<UpdateFacilityRequest>
{
    public UpdateFacilityRequestValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(255);
    }
}