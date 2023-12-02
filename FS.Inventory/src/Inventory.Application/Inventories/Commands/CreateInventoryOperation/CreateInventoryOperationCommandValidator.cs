using FluentValidation;
using Inventory.Domain.Repositories;
using SH.Infrastructure.Consts;

namespace Inventory.Application.Inventories.Commands.CreateInventoryOperation;

public class CreateInventoryOperationCommandValidator : AbstractValidator<CreateInventoryOperationCommand>
{
    public CreateInventoryOperationCommandValidator(IInventoryRepository repository)
    {
        Include(new InventoryIdValidator(repository));
        Include(new CountValidator());
        Include(new DescriptionValidator());
    }
}

internal class InventoryIdValidator : AbstractValidator<CreateInventoryOperationCommand>
{
    public InventoryIdValidator(IInventoryRepository repository)
    {
        RuleFor(_ => _.InventoryId).NotEmpty().WithMessage(ValidationMessage.NotEmpty).WithName("انبارداری")
            .NotEqual(Guid.Empty).WithMessage(ValidationMessage.NotEqual)
            .MustAsync(async (id, cancellationToken) => await repository.InventoryExists(id, cancellationToken))
            .WithMessage(ValidationMessage.NotExists);
    }
}

internal class CountValidator : AbstractValidator<CreateInventoryOperationCommand>
{
    public CountValidator()
    {
        RuleFor(_ => _.Count).NotEmpty().WithMessage(ValidationMessage.NotEmpty).WithName("تعداد");
    }
}

internal class DescriptionValidator : AbstractValidator<CreateInventoryOperationCommand>
{
    public DescriptionValidator()
    {
        RuleFor(_ => _.Description).NotEmpty().WithMessage(ValidationMessage.NotEmpty).WithName("توضیحات");
    }
}
