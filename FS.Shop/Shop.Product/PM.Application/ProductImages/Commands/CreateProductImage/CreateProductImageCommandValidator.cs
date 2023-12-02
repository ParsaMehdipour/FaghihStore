namespace PM.Application.ProductImages.Commands.CreateProductImage;

public class CreateProductImageCommandValidator : AbstractValidator<CreateProductImageCommand>
{
    public CreateProductImageCommandValidator()
    {
        RuleFor(_ => _.ProductId).NotEqual(Guid.Empty).WithMessage(ValidationMessage.NotEqual).WithName("محصول")
            .NotNull().WithMessage(ValidationMessage.NotEmpty);

        //TODO: write validation for check files extension.
        RuleFor(_ => _.Files).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .WithName("تصاویر");
    }
}