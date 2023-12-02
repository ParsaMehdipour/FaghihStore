namespace PB.Application.Brands.Commands.CreateBrand;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{

    public CreateBrandCommandValidator(IBrandRepository brandRepository)
    {
        Include(new TitleBrandValidator(brandRepository));

        Include(new SlugBrandValidator(brandRepository));

        Include(new OrderNumberBrandValidator(brandRepository));
    }
}


internal class TitleBrandValidator : AbstractValidator<CreateBrandCommand>
{
    public TitleBrandValidator(IBrandRepository brandRepository)
    {
        RuleFor(_ => _.Title)
            .MaximumLength(100).WithName("عنوان").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (brand, cancellationToken) => !await brandRepository.TitleBeUnique(Guid.Empty, brand, false, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}


internal class SlugBrandValidator : AbstractValidator<CreateBrandCommand>
{
    public SlugBrandValidator(IBrandRepository brandRepository)
    {
        RuleFor(_ => _.Slug)
            .MaximumLength(100).WithName("slug").WithMessage(ValidationMessage.MaximumLength)
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty)
            .MustAsync(async (slug, cancellationToken) => !await brandRepository.SlugBeUnique(Guid.Empty, slug, false, cancellationToken))
            .WithMessage(ValidationMessage.BeUnique);
    }
}

internal class OrderNumberBrandValidator : AbstractValidator<CreateBrandCommand>
{
    public OrderNumberBrandValidator(IBrandRepository brandRepository)
    {
        RuleFor(_ => _.OrderNumber)
            .NotEqual(0).WithName("اولویت").WithMessage(ValidationMessage.NotEqual)
            .MustAsync(async (orderNumber, cancellationToken) => !await brandRepository.OrderNumberBeUnique(Guid.Empty, orderNumber, false, cancellationToken)).WithMessage(ValidationMessage.BeUnique);
    }
}

