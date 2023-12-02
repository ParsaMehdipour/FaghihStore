namespace PM.Application.ProductVarieties.Commands.CreateProductVariety;

public class CreateProductVarietyCommandValidator : AbstractValidator<CreateProductVarietyCommand>
{
    public CreateProductVarietyCommandValidator()
    {
        RuleFor(_ => _.ProductId).NotEqual(Guid.Empty).WithMessage("آی دی محصول خالی می باشد");

        RuleFor(_ => _.VarietyId).NotEqual(Guid.Empty).WithMessage("آی دی نوع خالی می باشد");

        RuleFor(_ => _.InventoryId).NotEqual(Guid.Empty).WithMessage("آی دی انبار خالی می باشد");
    }
}
