using FluentValidation;

namespace VG.Application.Varieties.Commands.DeleteVariety;

public class DeleteVarietyCommandValidator : AbstractValidator<DeleteVarietyCommand>
{
    public DeleteVarietyCommandValidator()
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");
    }
}
