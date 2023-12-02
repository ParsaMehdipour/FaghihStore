using FluentResults;
using Inventory.Application.Inventories.Commands.EditInventory;
using MediatR;

namespace Inventory.Application.Inventories.Queries.GetInventory;

public record GetInventoryQuery(Guid Id) : IRequest<Result<EditInventoryCommand>>
{
    internal EditInventoryCommand ToCommand(Domain.Models.Inventory inventory) =>
        new(inventory.Id, inventory.UnitPrice, inventory.InStock);
}
