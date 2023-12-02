using FluentValidation;

namespace CD.Application.CountryDivisions.Commands.DeleteCountryDivision;

public class DeleteCountryDivisionCommandValidator : AbstractValidator<DeleteCountryDivisionCommand>
{
    public DeleteCountryDivisionCommandValidator()
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");
    }
}
