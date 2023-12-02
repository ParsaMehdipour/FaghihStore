using SH.Infrastructure.Criteria.Searching;

namespace PM.Application.Criteria;

public class ProductQueryStringParameters : QueryStringParameters
{
    //Filters
    public string FilterByTitlePersian { get; set; }
    public string FilterByTitleEnglish { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime? FilterByEndWarrantyDate { get; set; }

    public bool IsFilter()
    {
        if (!string.IsNullOrWhiteSpace(FilterByTitlePersian) ||
           !string.IsNullOrWhiteSpace(FilterByTitleEnglish) ||
           CategoryId != Guid.Empty ||
           FilterByEndWarrantyDate.HasValue)
            HasFilter = true;
        return HasFilter;
    }
}