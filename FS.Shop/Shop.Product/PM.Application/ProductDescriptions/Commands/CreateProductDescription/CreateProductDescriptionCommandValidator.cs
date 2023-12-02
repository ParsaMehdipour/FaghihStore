namespace PM.Application.ProductDescriptions.Commands.CreateProductDescription;

public class CreateProductDescriptionCommandValidator : AbstractValidator<CreateProductDescriptionCommand>
{
    public CreateProductDescriptionCommandValidator()
    {
        RuleFor(_ => _.Title).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .WithName("عنوان")
            .MaximumLength(50).WithMessage(ValidationMessage.MaximumLength);

        RuleFor(_ => _.Description).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .WithName("توضیحات");

        //TODO: should be validating ProductId.
    }
}