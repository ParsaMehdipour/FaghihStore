using PM.Domain.ProductImageAgg;

namespace PM.Application.ProductImages.Commands.SetThumbnailProductImage;

public class SetThumbnailProductImageCommandValidator : AbstractValidator<SetThumbnailProductImageCommand>
{
    public SetThumbnailProductImageCommandValidator(IProductImageRepository productImageRepository)
    {
        RuleFor(_ => _.Title).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(70).WithMessage(ValidationMessage.MaximumLength)
            .WithName("عنوان");

        RuleFor(_ => _.Alt).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(70).WithMessage(ValidationMessage.MaximumLength)
            .WithName("عنوان");

        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage(ValidationMessage.NotEqual).WithName("تصویر محصول")
            .NotNull().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (command, id, cancellationToken) => await productImageRepository.IsExistsAsync(pi => pi.Id.Equals(command.Id) && pi.ProductId.Equals(command.ProductId) && pi.IsThumbnail == false, cancellationToken)).WithMessage(ValidationMessage.NotExists);
    }
}