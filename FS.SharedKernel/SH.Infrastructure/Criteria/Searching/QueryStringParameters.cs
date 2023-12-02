namespace SH.Infrastructure.Criteria.Searching;

public abstract class QueryStringParameters
{
    const byte MAX_PAGE_SIZE = 50;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
    }

    public string OrderBy { get; set; }
    public string Search { get; set; }
    public bool IsDeleted { get; set; }
    protected bool HasFilter { get; set; }
}