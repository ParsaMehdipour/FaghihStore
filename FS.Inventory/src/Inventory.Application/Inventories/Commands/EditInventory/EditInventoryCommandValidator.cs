using FluentValidation;
using SH.Infrastructure.Consts;

namespace Inventory.Application.Inventories.Commands.EditInventory;

public class EditInventoryCommandValidator : AbstractValidator<EditInventoryCommand>
{
    public EditInventoryCommandValidator()
    {
        RuleFor(_ => _.Id).NotEqual(Guid.Empty).WithMessage("آی دی خالی می باشد");

        RuleFor(_ => _.UnitPrice).NotEmpty().WithMessage(ValidationMessage.NotEmpty).WithName("قیمت");
    }
}
