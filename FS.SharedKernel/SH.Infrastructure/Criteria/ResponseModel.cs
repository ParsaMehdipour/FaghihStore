using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Criteria.Searching;

namespace SH.Infrastructure.Criteria;

public class ResponseModel
{
    public QueryStringParameters Parameters { get; set; }
    public Pager Pager { get; set; }
}

public class ResponseModel<TModel> : ResponseModel
{
    public TModel Model { get; set; }
}

public class ResponseModel<TModel, TParameterModel> : ResponseModel where TParameterModel : QueryStringParameters
{
    public TModel Model { get; set; }
    public new TParameterModel Parameters { get; set; }
}