namespace TG.Application.TraitGroups.Commands.CreateTraitGroup;

public class CreateTraitGroupCommandValidator : AbstractValidator<CreateTraitGroupCommand>
{
    public CreateTraitGroupCommandValidator(ITraitGroupRepository repository)
    {
        RuleFor(_ => _.Title)
           .MaximumLength(maximumLength: 100).WithName(overridePropertyName: "عنوان").WithMessage(errorMessage: ValidationMessage.MaximumLength)
           .NotEmpty().WithMessage(errorMessage: ValidationMessage.NotEmpty)
           .MustAsync(async (title, cancellationToken) => !await repository.TitleExists(id: Guid.Empty, title: title, isForModify: false, cancellationToken: cancellationToken)).WithMessage(errorMessage: ValidationMessage.BeUnique);
    }
}