namespace VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

public class DeleteTraitGroupCommandValidator : AbstractValidator<DeleteTraitGroupCommand>
{
    public DeleteTraitGroupCommandValidator()
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");
    }
}