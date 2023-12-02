namespace FaghihstoreQuery.Models.Product.QueryFilter;

public record BrandQueryFilter
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsCurrent { get; set; }
}