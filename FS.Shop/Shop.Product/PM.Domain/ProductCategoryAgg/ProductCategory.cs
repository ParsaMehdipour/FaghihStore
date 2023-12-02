using PM.Domain.ProductAgg;

using SH.Domain;
using SH.Domain.Interfaces;

namespace PM.Domain.ProductCategoryAgg;

public class ProductCategory : AuditableEntity, IAggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Keywords { get; private set; }
    public string MetaDescription { get; private set; }
    public string Slug { get; private set; }
    public List<Product> Products { get; private set; }

    public ProductCategory()
    {
        Products = new List<Product>();
    }

    public ProductCategory(string name, string description, string keywords, string metaDescription,
        string slug)
    {
        Title = name;
        Description = description;
        Keywords = keywords;
        MetaDescription = metaDescription;
        Slug = slug;
    }

    public void Edit(string name, string description, string keywords, string metaDescription,
        string slug)
    {
        Title = name;
        Description = description;
        Keywords = keywords;
        MetaDescription = metaDescription;
        Slug = slug;
    }
}