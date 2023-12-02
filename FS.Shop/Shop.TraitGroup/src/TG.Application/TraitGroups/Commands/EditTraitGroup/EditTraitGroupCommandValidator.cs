namespace TG.Application.TraitGroups.Commands.EditTraitGroup;

public class EditTraitGroupCommandValidator : AbstractValidator<EditTraitGroupCommand>
{
    public EditTraitGroupCommandValidator(ITraitGroupRepository repository)
    {
        RuleFor(_ => _.Title)
           .MaximumLength(maximumLength: 100).WithName(overridePropertyName: "عنوان").WithMessage(errorMessage: ValidationMessage.MaximumLength)
           .NotEmpty().WithMessage(errorMessage: ValidationMessage.NotEmpty)
           .MustAsync(async (command, title, cancellationToken) => !await repository.TitleExists(id: command.Id, title: title, isForModify: true, cancellationToken: cancellationToken)).WithMessage(errorMessage: ValidationMessage.BeUnique);
    }
}