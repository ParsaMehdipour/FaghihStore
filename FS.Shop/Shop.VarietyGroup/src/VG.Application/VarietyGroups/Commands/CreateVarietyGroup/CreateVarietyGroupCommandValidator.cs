using FluentValidation;
using SH.Infrastructure.Consts;
using VG.Domain.Repositories;

namespace VG.Application.VarietyGroups.Commands.CreateVarietyGroup;

public class CreateVarietyGroupCommandValidator : AbstractValidator<CreateVarietyGroupCommand>
{
    public CreateVarietyGroupCommandValidator(IVarietyGroupRepository repository)
    {
        Include(new TitleVarietyGroupValidator(repository: repository));
    }
}

internal class TitleVarietyGroupValidator : AbstractValidator<CreateVarietyGroupCommand>
{
    public TitleVarietyGroupValidator(IVarietyGroupRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.Title)
            .MaximumLength(maximumLength: 100).WithName(overridePropertyName: "عنوان").WithMessage(errorMessage: ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(errorMessage: ValidationMessage.NotEmpty)
            .MustAsync(async (title, cancellationToken) => !await repository.TitleExists(id: Guid.Empty, title: title, isForModify: isForModify, cancellationToken: cancellationToken)).WithMessage(errorMessage: ValidationMessage.BeUnique); ;
    }
}
