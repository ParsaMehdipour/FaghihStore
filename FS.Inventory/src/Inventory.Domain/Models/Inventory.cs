using Inventory.Domain.Enums;
using SH.Domain;
using SH.Domain.Interfaces;

namespace Inventory.Domain.Models;

public class Inventory : AuditableEntity, IAggregateRoot
{
    public Guid ProductVarietyId { get; private set; }
    public long UnitPrice { get; private set; }
    public bool InStock { get; private set; }
    public List<InventoryOperation> Operations { get; private set; }

    private Inventory()
    {

    }

    public Inventory(Guid productVarietyId, long unitPrice, long count)
    {
        CreatedDate = DateTime.Now;
        SetProductVarietyId(productVarietyId);
        SetUnitPrice(unitPrice);
        PrimitiveOperation(count);
    }

    public void Edit(Guid productVarietyId, long unitPrice)
    {
        SetProductVarietyId(productVarietyId);
        SetUnitPrice(unitPrice);
    }

    public void PrimitiveOperation(long count)
    {
        if (count == 0)
            return;
        AddOperation(new(InventoryOperationType.Increased, count, Guid.Empty, count, "موجودی اولیه", Guid.Empty, this.Id));
        SetInStock(true);
    }

    public long CalculateCurrentCount()
    {
        var plus = Operations.Where(x => x.OperationType == InventoryOperationType.Increased).Sum(x => x.Count);
        var minus = Operations.Where(x => x.OperationType == InventoryOperationType.Reduce).Sum(x => x.Count);
        return plus - minus;
    }

    public void Increase(long count, Guid operatorId, string description)
    {
        var currentCount = CalculateCurrentCount() + count;
        var operation = new InventoryOperation(InventoryOperationType.Increased, count, operatorId, currentCount, description, Guid.Empty, Id);
        AddOperation(operation);

        //if (currentCount > 0)
        //    InStock = true;
        //else
        //    InStock = false;

        SetInStock(currentCount > 0);
    }

    public void Reduce(long count, Guid operatorId, string description, Guid orderId)
    {
        var currentCount = CalculateCurrentCount() - count;
        var operation = new InventoryOperation(InventoryOperationType.Reduce, count, operatorId, currentCount, description, orderId, Id);
        AddOperation(operation);
        SetInStock(currentCount > 0);
    }

    public void SetProductVarietyId(Guid productVarietyId)
    {
        if (ProductVarietyId == productVarietyId)
            return;

        ProductVarietyId = productVarietyId;
    }

    public void SetUnitPrice(long unitPrice)
    {
        if (UnitPrice == unitPrice)
            return;

        UnitPrice = unitPrice;
    }

    public void SetInStock(bool inStock)
    {
        if (InStock == inStock)
            return;

        InStock = inStock;
    }

    public void AddOperation(InventoryOperation operation)
    {
        Operations ??= new List<InventoryOperation>();

        Operations.Add(operation);
    }

}