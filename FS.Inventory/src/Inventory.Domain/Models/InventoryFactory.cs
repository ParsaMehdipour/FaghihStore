namespace Inventory.Domain.Models;

public class InventoryFactory
{
    public Inventory Create(Guid productVarietyId, long unitPrice, long count)
    {
        Inventory inventory = new(productVarietyId, unitPrice, count);

        return inventory;
    }
}
