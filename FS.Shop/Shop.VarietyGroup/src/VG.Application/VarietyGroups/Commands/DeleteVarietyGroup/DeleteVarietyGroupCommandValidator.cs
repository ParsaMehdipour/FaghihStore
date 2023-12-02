using FluentValidation;

namespace VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

public class DeleteVarietyGroupCommandValidator : AbstractValidator<DeleteVarietyGroupCommand>
{
    public DeleteVarietyGroupCommandValidator()
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");
    }
}
