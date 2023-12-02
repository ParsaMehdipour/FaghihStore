using CD.Domain.Repositories;
using FluentValidation;
using SH.Infrastructure.Consts;

namespace CD.Application.CountryDivisions.Commands.CreateCountryDivision;

internal class CreateCountryDivisionCommandValidator : AbstractValidator<CreateCountryDivisionCommand>
{
    public CreateCountryDivisionCommandValidator(ICountryDivisionRepository repository, bool isForModify)
    {
        Include(new NameValidator(repository, isForModify));

        Include(new ParentIdValidator(repository, isForModify));
    }
}

internal class NameValidator : AbstractValidator<CreateCountryDivisionCommand>
{
    public NameValidator(ICountryDivisionRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.Name)
            .MaximumLength(100).WithName("نام").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (name, cancellationToken) => !await repository.NameIsExists(Guid.Empty, name, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}

internal class ParentIdValidator : AbstractValidator<CreateCountryDivisionCommand>
{
    public ParentIdValidator(ICountryDivisionRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.ParentId)
            .MustAsync(async (parentId, cancellationToken) => !await repository.ParentIsExists(Guid.Empty, parentId, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique);

    }
}
