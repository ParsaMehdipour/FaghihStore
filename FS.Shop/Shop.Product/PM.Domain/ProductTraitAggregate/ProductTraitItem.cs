using PM.Domain.ProductAgg;

using SH.Domain;
using SH.Domain.Interfaces;

namespace PM.Domain.ProductTraitAggregate;
public class ProductTraitItem : AuditableEntity, IAggregateRoot
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public Guid TraitId { get; private set; }

    public string Value { get; private set; }
    public int OrderNumber { get; private set; }

    /// <summary>
    /// this property's mean to show this <see cref="ProductTraitItem"/> record in general product specifications section.
    /// </summary>
    public bool HasInGeneralSpecification { get; private set; }

    internal ProductTraitItem(string value, int orderNumber, bool hasInGeneralSpecification, Guid productId, Guid traitId)
    {
        SetValue(value);
        SetOrderNumber(orderNumber);
        SetHasInGeneralSpecification(hasInGeneralSpecification);
        SetProductId(productId);
        SetTraitId(traitId);
        CreatedDate = DateTime.Now;
    }

    public void SetValue(string value)
    {
        if (Value == value)
            return;

        Value = value;
    }

    public void SetOrderNumber(int orderNumber)
    {
        if (OrderNumber == orderNumber)
            return;
        OrderNumber = orderNumber;
    }

    public void SetProductId(Guid productId)
    {
        if (ProductId == productId)
            return;

        ProductId = productId;
    }

    public void SetTraitId(Guid traitId)
    {
        if (TraitId == traitId)
            return;

        TraitId = traitId;
    }

    public void SetHasInGeneralSpecification(bool hasInGeneralSpecification)
    {
        if (HasInGeneralSpecification == hasInGeneralSpecification)
            return;

        HasInGeneralSpecification = hasInGeneralSpecification;
    }

    public void SetModifiedDate(DateTime modifiedDate)
    {
        if (ModifiedDate != modifiedDate)
            ModifiedDate = modifiedDate;
    }
}