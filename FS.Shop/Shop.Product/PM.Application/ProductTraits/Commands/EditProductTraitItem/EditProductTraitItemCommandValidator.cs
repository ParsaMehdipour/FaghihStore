using PM.Domain.ProductTraitAggregate;

namespace PM.Application.ProductTraits.Commands.EditProductTraitItem;

public class EditProductTraitItemCommandValidator : AbstractValidator<EditProductTraitItemCommand>
{
    public EditProductTraitItemCommandValidator(IProductTraitItemRepository repository)
    {
        RuleFor(_ => _.Value)
                   .MaximumLength(maximumLength: 20).WithMessage(errorMessage: ValidationMessage.MaximumLength)
                   .WithName(overridePropertyName: "مقدار")
                   .NotEmpty().WithMessage(errorMessage: ValidationMessage.NotEmpty)
                   .MustAsync(async (command, title, cancellationToken) =>
                   !await repository.ValueExists(id: command.Id, title, isForModify: true, cancellationToken)).WithMessage(errorMessage: ValidationMessage.BeUnique);
    }
}