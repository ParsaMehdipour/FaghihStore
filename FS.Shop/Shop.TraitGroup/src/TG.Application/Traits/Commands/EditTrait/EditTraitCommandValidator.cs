namespace TG.Application.Traits.Commands.EditTrait;

public class EditTraitCommandValidator : AbstractValidator<EditTraitCommand>
{
    public EditTraitCommandValidator(ITraitRepository repository)
    {
        RuleFor(_ => _.Title)
           .MaximumLength(maximumLength: 100).WithMessage(errorMessage: ValidationMessage.MaximumLength)
           .WithName(overridePropertyName: "عنوان")
           .NotEmpty().WithMessage(errorMessage: ValidationMessage.NotEmpty)
           .MustAsync(async (command, title, cancellationToken) => !await repository.TitleExists(id: command.Id, title, isForModify: true, cancellationToken)).WithMessage(errorMessage: ValidationMessage.BeUnique);

        RuleFor(_ => _.TraitGroupId)
            .MustAsync(repository.TraitGroupIdExists).WithMessage(errorMessage: ValidationMessage.NotExists)
            .WithName("گروه ویژگی");

        RuleFor(_ => _.CategoryId)
            .MustAsync(repository.CategoryIdExists).WithMessage(errorMessage: ValidationMessage.NotExists)
            .WithName("دسته بندی");
    }
}