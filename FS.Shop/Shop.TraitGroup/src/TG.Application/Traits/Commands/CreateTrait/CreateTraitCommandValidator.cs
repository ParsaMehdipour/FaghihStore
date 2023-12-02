namespace TG.Application.Traits.Commands.CreateTrait;

public class CreateTraitCommandValidator : AbstractValidator<CreateTraitCommand>
{
    public CreateTraitCommandValidator(ITraitRepository repository)
    {
        RuleFor(_ => _.Title)
           .MaximumLength(maximumLength: 100).WithMessage(errorMessage: ValidationMessage.MaximumLength)
           .WithName(overridePropertyName: "عنوان")
           .NotEmpty().WithMessage(errorMessage: ValidationMessage.NotEmpty)
           .MustAsync(async (title, cancellationToken) => !await repository.TitleExists(id: Guid.Empty, title, isForModify: false, cancellationToken)).WithMessage(errorMessage: ValidationMessage.BeUnique);

        RuleFor(_ => _.TraitGroupId)
            .MustAsync(repository.TraitGroupIdExists).WithMessage(errorMessage: ValidationMessage.NotExists)
            .WithName("گروه ویژگی");

        RuleFor(_ => _.CategoryId)
            .MustAsync(repository.CategoryIdExists).WithMessage(errorMessage: ValidationMessage.NotExists)
            .WithName("دسته بندی");
    }
}