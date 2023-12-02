using FaghihstoreQuery.Models.Product.QueryFilter;
using SH.Infrastructure.Criteria.Searching;

namespace FaghihstoreQuery.Models.Product.SerachModel;
public class ProductSearchModel : QueryStringParameters
{
    public IEnumerable<BrandQueryFilter> BrandQueryFilters { get; set; } = new List<BrandQueryFilter>();
    //public Guid[] VarietiesIds { get; set; } = { Guid.Empty };
    public bool InStock { get; set; }
    public long MinPrice { get; set; } = 0;
    public long MaxPrice { get; set; } = 100000000;

    public bool IsFilter()
    {
        if (BrandQueryFilters.Any(_ => _.IsCurrent))
            HasFilter = true;

        return HasFilter;
    }
}
