using PM.Domain.ProductTraitAggregate;

namespace PM.Application.ProductTraits.Commands.CreateProductTraitItem;

public class CreateProductTraitItemCommandValidator : AbstractValidator<CreateProductTraitItemCommand>
{
    public CreateProductTraitItemCommandValidator(IProductTraitItemRepository repository)
    {
        RuleFor(_ => _.Value)
                   .MaximumLength(maximumLength: 20).WithMessage(errorMessage: ValidationMessage.MaximumLength)
                   .WithName(overridePropertyName: "مقدار")
                   .NotEmpty().WithMessage(errorMessage: ValidationMessage.NotEmpty)
                   .MustAsync(async (title, cancellationToken) =>
                   !await repository.ValueExists(id: Guid.Empty, title, isForModify: false, cancellationToken)).WithMessage(errorMessage: ValidationMessage.BeUnique);
    }
}