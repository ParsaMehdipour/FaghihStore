using FluentResults;
using Inventory.Domain.Enums;
using Inventory.Domain.Repositories;
using MediatR;
using SH.Application.Interfaces;

namespace Inventory.Application.Inventories.Commands.CreateInventoryOperation;

public class CreateInventoryOperationCommandHandler : IRequestHandler<CreateInventoryOperationCommand, Result<Guid>>
{
    protected IInventoryRepository _inventoryRepository { get; }
    protected ICurrentUserService _currentUserService { get; }

    public CreateInventoryOperationCommandHandler(IInventoryRepository inventoryRepository, ICurrentUserService currentUserService)
    {
        _inventoryRepository = inventoryRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(CreateInventoryOperationCommand request, CancellationToken cancellationToken)
    {
        Domain.Models.Inventory inventory = await _inventoryRepository.GetByIdAsync(cancellationToken, request.InventoryId);

        ArgumentNullException.ThrowIfNull(inventory, nameof(inventory));

        if (request.OperationType == InventoryOperationType.Increased)
            inventory.Increase(request.Count, _currentUserService.UserId.Value, request.Description);
        else
            inventory.Reduce(request.Count, _currentUserService.UserId.Value, request.Description, Guid.Empty);

        await _inventoryRepository.SaveAsync(cancellationToken);

        return Result.Ok(inventory.Id);
    }
}
