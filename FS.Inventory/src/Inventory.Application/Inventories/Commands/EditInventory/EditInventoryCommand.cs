using FluentResults;
using MediatR;

namespace Inventory.Application.Inventories.Commands.EditInventory;

public record EditInventoryCommand(Guid Id, long UnitPrice, bool InStock) : IRequest<Result>;
