using PM.Domain.ProductAgg;
using SH.Domain;
using SH.Domain.Interfaces;

namespace PM.Domain.ProductDescriptionAgg;

public class ProductDescription : AuditableEntity, IAggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    internal ProductDescription(string title, string description, Guid productId)
    {
        SetTitle(title);
        SetDescription(description);
        SetProductId(productId);
    }

    public void SetTitle(string title)
    {
        if (Title == title)
            return;

        Title = title;
    }

    public void SetDescription(string description)
    {
        if (Description == description)
            return;

        Description = description;
    }

    public void SetProductId(Guid productId)
    {
        if (ProductId == productId)
            return;

        ProductId = productId;
    }
}