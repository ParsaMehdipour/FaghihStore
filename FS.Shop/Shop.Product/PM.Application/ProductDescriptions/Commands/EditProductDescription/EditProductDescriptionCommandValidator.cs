using PM.Domain.ProductDescriptionAgg;

namespace PM.Application.ProductDescriptions.Commands.EditProductDescription;

public class EditProductDescriptionCommandValidator : AbstractValidator<ProductDescription>
{

    public EditProductDescriptionCommandValidator()
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");

        RuleFor(_ => _.Title).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .WithName("عنوان")
            .MaximumLength(50).WithMessage(ValidationMessage.MaximumLength);

        RuleFor(_ => _.Description).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .WithName("توضیحات");

        //TODO: should be validating ProductId.
    }

}
