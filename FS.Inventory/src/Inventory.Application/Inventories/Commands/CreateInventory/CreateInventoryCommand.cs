using FluentResults;
using MediatR;

namespace Inventory.Application.Inventories.Commands.CreateInventory;

public record CreateInventoryCommand(Guid VarietyId, Guid ProductId, long UnitPrice, long Count) : IRequest<Result<Guid>>;