namespace PM.Application.Products.Commands.EditProduct;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(_ => _.TitlePersian).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(250).WithMessage(ValidationMessage.MaximumLength)
            .WithName("عنوان فارسی");

        RuleFor(_ => _.TitleEnglish).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(250).WithMessage(ValidationMessage.MaximumLength)
            .WithName("عنوان انگلیسی");

        RuleFor(_ => _.MetaDescription).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(150).WithMessage(ValidationMessage.MaximumLength)
            .WithName("توضیحات متا");

        RuleFor(_ => _.Slug).NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MaximumLength(500).WithMessage(ValidationMessage.MaximumLength)
            .WithName("Slug");

        RuleFor(_ => _.WarrantyDescription).MaximumLength(500).WithMessage(ValidationMessage.MaximumLength)
            .WithName("توضیحات گارانتی");

        RuleFor(_ => _.CategoryId)
            .NotEqual(Guid.Empty).WithMessage(ValidationMessage.NotEmpty)
            .WithName("دسته بندی");

        RuleFor(_ => _.BrandId)
            .NotEqual(Guid.Empty).WithMessage(ValidationMessage.NotEmpty)
            .WithName("برند");
    }
}