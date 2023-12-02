using FluentValidation;
using Inventory.Domain.Repositories;
using SH.Infrastructure.Consts;

namespace Inventory.Application.Inventories.Commands.CreateInventory;

public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator(IInventoryRepository repository)
    {
        Include(new ProductVarietyValidator(repository));
        Include(new ProductIdValidator());
        Include(new UnitPriceValidator());
        Include(new CountValidator());
    }
}

internal class ProductVarietyValidator : AbstractValidator<CreateInventoryCommand>
{
    public ProductVarietyValidator(IInventoryRepository repository)
    {
        RuleFor(_ => _.VarietyId).NotEqual(Guid.Empty).WithMessage(ValidationMessage.NotEqual).WithName("نوع");
        //todo: Must check whether the same product with the same variety exists
    }
}

internal class UnitPriceValidator : AbstractValidator<CreateInventoryCommand>
{
    public UnitPriceValidator()
    {
        RuleFor(_ => _.UnitPrice).NotEmpty().WithMessage(ValidationMessage.NotEmpty).WithName("قیمت");
    }
}

internal class CountValidator : AbstractValidator<CreateInventoryCommand>
{
    public CountValidator()
    {
        RuleFor(_ => _.Count).NotEmpty().WithMessage(ValidationMessage.NotEmpty).WithName("تعداد");
    }
}

internal class ProductIdValidator : AbstractValidator<CreateInventoryCommand>
{
    public ProductIdValidator()
    {
        RuleFor(_ => _.ProductId).NotEqual(Guid.Empty).WithMessage(ValidationMessage.NotEqual).WithName("محصول")
            .NotEmpty().WithMessage(ValidationMessage.NotEmpty);
    }
}