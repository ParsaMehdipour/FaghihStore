namespace PB.Application.Brands.Commands.EditBrand;

public class EditBrandCommandValidator : AbstractValidator<EditBrandCommand>
{
    public EditBrandCommandValidator(IBrandRepository brandRepository)
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");

        RuleFor(_ => _.Title)
            .MaximumLength(100).WithName("عنوان").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, brand, cancellationToken) => !await brandRepository.TitleBeUnique(command.Id, command.Title, true, cancellationToken)).WithMessage(ValidationMessage.BeUnique);

        RuleFor(_ => _.Slug)
            .MaximumLength(100).WithName("slug").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, slug, cancellationToken) => !await brandRepository.SlugBeUnique(command.Id, command.Slug, true, cancellationToken))
            .WithMessage(ValidationMessage.BeUnique);

        RuleFor(_ => _.OrderNumber)
            .NotEqual(0).WithName("اولویت").WithMessage(ValidationMessage.NotEqual)
            .MustAsync(async (command, orderNumber, cancellationToken) => !await brandRepository.OrderNumberBeUnique(command.Id, command.OrderNumber, true, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}
