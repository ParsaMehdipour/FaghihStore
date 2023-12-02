using CD.Domain.Repositories;

using FluentValidation;

using SH.Infrastructure.Consts;

namespace CD.Application.CountryDivisions.Commands.EditCountryDivision;

public class EditCountryDivisionCommandValidator : AbstractValidator<EditCountryDivisionCommand>
{
    public EditCountryDivisionCommandValidator(ICountryDivisionRepository repository)
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");

        RuleFor(_ => _.Name)
            .MaximumLength(100).WithName("نام").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, name, cancellationToken) => !await repository.NameIsExists(command.Id, command.Name, true, cancellationToken)).WithMessage(ValidationMessage.NotExists).WithName("نام");

        RuleFor(_ => _.ParentId)
            .MustAsync(async (command, parent, cancellationToken) => await repository.ParentIsExists(command.Id, command.ParentId, true, cancellationToken))
            .WithMessage(ValidationMessage.NotExists).WithName("زیر مجموعه");
    }
}
