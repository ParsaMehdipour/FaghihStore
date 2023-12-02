using SH.Infrastructure.Criteria.Searching;

namespace Inventory.Application.Criteria;
public class InventoryQueryStringParameters : QueryStringParameters
{
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
    public string VarietyTitle { get; set; }
    public bool InStock { get; set; }

    public bool IsFilter()
    {
        if (!string.IsNullOrWhiteSpace(ProductTitle)
            || ProductId != Guid.Empty)
            HasFilter = true;

        return HasFilter;
    }
}
