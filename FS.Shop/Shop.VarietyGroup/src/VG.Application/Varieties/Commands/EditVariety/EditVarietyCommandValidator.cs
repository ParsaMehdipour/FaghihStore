using FluentValidation;
using SH.Infrastructure.Consts;
using VG.Domain.Enums;
using VG.Domain.Repositories;

namespace VG.Application.Varieties.Commands.EditVariety;

public class EditVarietyCommandValidator : AbstractValidator<EditVarietyCommand>
{
    public EditVarietyCommandValidator(IVarietyRepository varietyRepository)
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");

        RuleFor(_ => _.VarietyGroupId).NotEqual(Guid.Empty).WithMessage("گروه نوع خالی می باشد");

        RuleFor(_ => _.Title)
            .MaximumLength(100).WithName("عنوان").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, title, cancellationToken) => !await varietyRepository.TitleExists(command.Id, command.Title, true, cancellationToken)).WithMessage(ValidationMessage.NotExists).WithName("عنوان");

        RuleFor(_ => _.ColorCode)
            .MaximumLength(100).WithName("کد رنگ").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty).When(_ => _.BoxType == BoxType.Circle)
            .MustAsync(async (command, colorCode, cancellationToken) => !await varietyRepository.ColorCodeExists(command.Id, command.ColorCode, true, cancellationToken)).WithMessage(ValidationMessage.NotExists).WithName("کد رنگ").When(_ => _.BoxType == BoxType.Circle);

        RuleFor(_ => _.Size)
            .MaximumLength(100).WithName("سایز").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty).When(_ => _.BoxType == BoxType.SelectionBox)
            .MustAsync(async (command, size, cancellationToken) => !await varietyRepository.SizeExists(command.Id, command.Size, true, cancellationToken)).WithMessage(ValidationMessage.NotExists).WithName("سایز").When(_ => _.BoxType == BoxType.SelectionBox);
    }
}
