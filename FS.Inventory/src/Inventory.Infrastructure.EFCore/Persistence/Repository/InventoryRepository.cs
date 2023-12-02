using Inventory.Domain.Repositories;

using PM.Infrastructure.EFCore;

using SH.Infrastructure.EfCore.Repositories;

namespace Inventory.Infrastructure.EFCore.Persistence.Repository;

public class InventoryRepository : EfRepository<Domain.Models.Inventory>, IInventoryRepository
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext;

    public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
    {
        _shopContext = shopContext;
        _inventoryContext = inventoryContext;
    }

    public Domain.Models.Inventory GetBy(Guid productId)
    {
        return _inventoryContext.Inventories.FirstOrDefault(/*x => x.ProductVarietyId == productId*/);
    }

    public async Task<bool> InventoryExists(Guid id, CancellationToken cancellationToken)
    {
        return await this.IsExistsAsync(_ => _.Id.Equals(id), cancellationToken);
    }

    public async Task<bool> ProductVarietyExists(Guid id, Guid productVarietyId, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists = false;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.ProductVarietyId.Equals(productVarietyId), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.ProductVarietyId.Equals(productVarietyId), cancellationToken);

        return isExists;
    }
}