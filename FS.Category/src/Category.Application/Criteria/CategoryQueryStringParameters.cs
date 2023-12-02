using SH.Infrastructure.Criteria.Searching;

namespace Category.Application.Criteria;

public class CategoryQueryStringParameters : QueryStringParameters
{
    public string Title { get; set; }
    public Guid? ParentId { get; set; }
}
