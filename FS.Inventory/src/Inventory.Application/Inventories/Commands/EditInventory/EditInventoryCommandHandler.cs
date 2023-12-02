using FluentResults;
using Inventory.Domain.Repositories;
using MediatR;

namespace Inventory.Application.Inventories.Commands.EditInventory;
public class EditInventoryCommandHandler : IRequestHandler<EditInventoryCommand, Result>
{
    protected IInventoryRepository _inventoryRepository { get; }

    public EditInventoryCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Result> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(inventory);

        inventory.SetUnitPrice(request.UnitPrice);
        inventory.SetInStock(request.InStock);

        await _inventoryRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
