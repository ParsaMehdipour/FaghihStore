using FluentResults;
using Inventory.Application.Inventories.Commands.EditInventory;
using Inventory.Domain.Repositories;
using MediatR;

namespace Inventory.Application.Inventories.Queries.GetInventory;

public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, Result<EditInventoryCommand>>
{
    protected IInventoryRepository _inventoryRepository { get; }

    public GetInventoryQueryHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Result<EditInventoryCommand>> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(inventory, nameof(inventory));

        return Result.Ok(request.ToCommand(inventory));
    }
}
