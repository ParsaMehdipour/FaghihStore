using PM.Domain.ProductAgg;
using SH.Domain;
using SH.Domain.Interfaces;

namespace PM.Domain.ProductVarietyAggregate;

public class ProductVariety : AuditableEntity, IAggregateRoot
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public Guid VarietyId { get; private set; }

    public Guid InventoryId { get; private set; }

    internal ProductVariety(Guid productId, Guid varietyId, Guid inventoryId)
    {
        CreatedDate = DateTime.Now;
        SetProductId(productId);
        SetVarietyId(varietyId);
        SetInventoryId(inventoryId);
    }

    public void SetProductId(Guid productId)
    {
        if (ProductId == productId)
            return;

        ProductId = productId;
    }

    public void SetVarietyId(Guid varietyId)
    {
        if (VarietyId == varietyId)
            return;

        VarietyId = varietyId;
    }

    public void SetInventoryId(Guid inventoryId)
    {
        if (InventoryId == inventoryId)
            return;

        InventoryId = inventoryId;
    }
}
