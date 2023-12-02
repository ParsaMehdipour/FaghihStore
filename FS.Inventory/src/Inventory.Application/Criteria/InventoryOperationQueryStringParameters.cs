using SH.Infrastructure.Criteria.Searching;

namespace Inventory.Application.Criteria;

public class InventoryOperationQueryStringParameters : QueryStringParameters
{
    public string Description { get; set; }
    public long Count { get; set; }

    bool IsFilter()
    {
        if (!string.IsNullOrWhiteSpace(Description) || Count > 0)
            return true;

        return false;
    }
}
