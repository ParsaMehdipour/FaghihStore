using Inventory.Domain.Enums;
using SH.Domain;
using SH.Domain.Interfaces;

namespace Inventory.Domain.Models;

public class InventoryOperation : AuditableEntity, IAggregateRoot
{
    public InventoryOperationType OperationType { get; private set; }
    public long Count { get; private set; }
    public Guid OperatorId { get; private set; }
    public DateTime OperationDate { get; private set; }
    public long CurrentCount { get; private set; }
    public string Description { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid InventoryId { get; private set; }
    public Inventory Inventory { get; private set; }
    protected InventoryOperation() { }

    internal InventoryOperation(InventoryOperationType operationType, long count, Guid operatorId, long currentCount,
        string description, Guid orderId, Guid inventoryId)
    {
        CreatedDate = DateTime.Now;
        SetOperation(operationType);
        SetCount(count);
        SetOperatorId(operatorId);
        SetCurrentCount(currentCount);
        SetDescription(description);
        SetOrderId(orderId);
        SetInventoryId(inventoryId);
        SetOperationDate(DateTime.Now);
    }

    public void SetOperation(InventoryOperationType operationType)
    {
        if (OperationType == operationType)
            return;

        OperationType = operationType;
    }

    public void SetCount(long count)
    {
        if (Count == count)
            return;

        Count = count;
    }

    public void SetOperatorId(Guid operatorId)
    {
        if (OperatorId == operatorId)
            return;

        OperatorId = operatorId;
    }

    public void SetCurrentCount(long currentCount)
    {
        if (CurrentCount == currentCount)
            return;

        CurrentCount = currentCount;
    }

    public void SetDescription(string description)
    {
        if (Description == description)
            return;

        Description = description;
    }

    public void SetOrderId(Guid orderId)
    {
        if (OrderId == orderId)
            return;

        OrderId = orderId;
    }

    public void SetInventoryId(Guid inventoryId)
    {
        if (InventoryId == inventoryId)
            return;

        InventoryId = inventoryId;
    }

    public void SetOperationDate(DateTime operationDate)
    {
        if (OperationDate == operationDate)
            return;

        OperationDate = operationDate;
    }
}

