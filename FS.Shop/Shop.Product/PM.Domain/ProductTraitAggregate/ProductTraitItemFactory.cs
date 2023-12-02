namespace PM.Domain.ProductTraitAggregate;

public class ProductTraitItemFactory
{
    public ProductTraitItem Create(string value, int orderNumber, bool hasInGeneralSpecification, Guid productId, Guid traitId)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentNullException.ThrowIfNull(productId);
        ArgumentNullException.ThrowIfNull(traitId);

        return new(value, orderNumber, hasInGeneralSpecification, productId, traitId);
    }
}