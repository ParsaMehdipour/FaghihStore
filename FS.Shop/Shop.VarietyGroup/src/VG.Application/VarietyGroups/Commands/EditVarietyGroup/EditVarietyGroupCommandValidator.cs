using FluentValidation;
using SH.Infrastructure.Consts;
using VG.Domain.Repositories;

namespace VG.Application.VarietyGroups.Commands.EditVarietyGroup;

public class EditVarietyGroupCommandValidator : AbstractValidator<EditVarietyGroupCommand>
{
    public EditVarietyGroupCommandValidator(IVarietyGroupRepository varietyGroupRepository)
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");

        RuleFor(_ => _.Title)
            .MaximumLength(100).WithName("عنوان").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, title, cancellationToken) => !await varietyGroupRepository.TitleExists(command.Id, command.Title, true, cancellationToken)).WithMessage(ValidationMessage.NotExists).WithName("عنوان");

    }
}
