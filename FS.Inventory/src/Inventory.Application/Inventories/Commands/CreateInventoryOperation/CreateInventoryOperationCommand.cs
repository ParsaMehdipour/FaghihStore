using FluentResults;
using Inventory.Domain.Enums;
using MediatR;

namespace Inventory.Application.Inventories.Commands.CreateInventoryOperation;

public record CreateInventoryOperationCommand(Guid InventoryId, InventoryOperationType OperationType, long Count, string Description) : IRequest<Result<Guid>>;