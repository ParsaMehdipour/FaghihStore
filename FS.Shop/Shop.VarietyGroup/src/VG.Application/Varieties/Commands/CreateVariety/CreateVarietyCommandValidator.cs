using FluentValidation;
using SH.Infrastructure.Consts;
using VG.Domain.Enums;
using VG.Domain.Repositories;

namespace VG.Application.Varieties.Commands.CreateVariety;

public class CreateVarietyCommandValidator : AbstractValidator<CreateVarietyCommand>
{
    public CreateVarietyCommandValidator(IVarietyRepository repository)
    {
        Include(new VarietyGroupIdValidator());
        Include(new TitleValidator(repository));
        Include(new ColorCodeValidator(repository));
        Include(new SizeValidator(repository));
    }
}

internal class VarietyGroupIdValidator : AbstractValidator<CreateVarietyCommand>
{
    public VarietyGroupIdValidator()
    {
        RuleFor(_ => _.VarietyGroupId).NotEqual(Guid.Empty).WithMessage("گروه نوع خالی می باشد");
    }
}

internal class TitleValidator : AbstractValidator<CreateVarietyCommand>
{
    public TitleValidator(IVarietyRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.Title)
            .MaximumLength(100).WithName("عنوان").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (title, cancellationToken) => !await repository.TitleExists(Guid.Empty, title, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}

internal class ColorCodeValidator : AbstractValidator<CreateVarietyCommand>
{
    public ColorCodeValidator(IVarietyRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.ColorCode)
            .MaximumLength(100).WithName("کد رنگ").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty).When(_ => _.BoxType == BoxType.Circle)
            .MustAsync(async (colorCode, cancellationToken) => !await repository.ColorCodeExists(Guid.Empty, colorCode, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique).When(_ => _.BoxType == BoxType.Circle);
    }
}

internal class SizeValidator : AbstractValidator<CreateVarietyCommand>
{
    public SizeValidator(IVarietyRepository repository, bool isForModify = false)
    {
        RuleFor(_ => _.Size)
            .MaximumLength(100).WithName("سایز").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty).When(_ => _.BoxType == BoxType.SelectionBox)
            .MustAsync(async (size, cancellationToken) => !await repository.SizeExists(Guid.Empty, size, isForModify, cancellationToken)).WithMessage(ValidationMessage.BeUnique).When(_ => _.BoxType == BoxType.SelectionBox);
    }
}
