namespace VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

public class DeleteProductTraitItemCommandValidator : AbstractValidator<DeleteProductTraitItemCommand>
{
    public DeleteProductTraitItemCommandValidator()
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");
    }
}