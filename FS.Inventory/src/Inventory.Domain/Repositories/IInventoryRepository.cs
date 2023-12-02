using SH.Domain.Interfaces;

namespace Inventory.Domain.Repositories;

public interface IInventoryRepository : IBaseRepository<Models.Inventory>
{
    Task<bool> ProductVarietyExists(Guid id, Guid productVarietyId, bool isForModify, CancellationToken cancellationToken);
    Task<bool> InventoryExists(Guid id, CancellationToken cancellationToken);
}