namespace FacilitiesCoordinator.API.Features.Facilities.Read;

using FluentValidation;

public sealed class ReadSingleFacilityRequestValidator : AbstractValidator<ReadSingleFacilityRequest>
{
    public ReadSingleFacilityRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(4);
    }
}