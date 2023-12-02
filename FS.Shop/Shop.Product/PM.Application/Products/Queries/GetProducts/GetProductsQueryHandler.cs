using DNTPersianUtils.Core;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using PM.Application.Criteria;
using PM.Domain.ProductAgg;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;
using SH.Infrastructure.Services;
using SH.Infrastructure.Settings;

namespace PM.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<ResponseModel<IEnumerable<GetProductsDto>, ProductQueryStringParameters>>>
{
    protected IProductRepository _productRepository { get; }
    protected HttpClientService _httpClientService { get; }
    protected List<ProjectsUrls> ProjectsUrls { get; }

    public GetProductsQueryHandler(IProductRepository productRepository,
        HttpClientService httpClientService,
        IOptions<SiteSettings> options)
    {
        _productRepository = productRepository;
        _httpClientService = httpClientService;
        ProjectsUrls = options.Value.ProjectsUrls;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetProductsDto>, ProductQueryStringParameters>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _productRepository.Get().IgnoreQueryFilters().Where(_ => _.IsDeleted == request.Parameters.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            products = products.Where(_ => _.TitlePersian.Contains(request.Parameters.Search) ||
                                       _.TitleEnglish.Contains(request.Parameters.Search));

        #region Filter

        if (request.Parameters.IsFilter())
        {
            if (!string.IsNullOrWhiteSpace(request.Parameters.FilterByTitlePersian))
                products = products.Where(_ => _.TitlePersian.Contains(request.Parameters.FilterByTitlePersian) ||
                                           _.TitleEnglish.Contains(request.Parameters.FilterByTitleEnglish));
            if (request.Parameters.CategoryId != Guid.Empty)
                products = products.Where(_ => _.CategoryId == request.Parameters.CategoryId);
        }

        #endregion

        int count = await products.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        var productCategories = products.ToDictionary(_ => _.Id, _ => _.CategoryId);

        _httpClientService.SetBaseAddress(ProjectsUrls.FirstOrDefault(_ => _.Project.Equals("Category.Api")).Url);
        var categories = await _httpClientService.Send<Dictionary<Guid, Guid>, Dictionary<Guid, string>>(productCategories, "api/category/GetProductCategories/", cancellationToken);

        var result = await products
            .Include(product => product.Images.Where(image => image.IsThumbnail))
            .OrderByDescending(_ => _.CreatedDate).Paginate(pager)
            .Select(_ => new GetProductsDto(
                    _.Id,
                    _.TitlePersian,
                    _.CreatedDate.ToShortPersianDateString(true),
                    _.GetThumbnailImage(),
                    categories[_.Id],
                    _.Descriptions.Count
          )).ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetProductsDto>, ProductQueryStringParameters> responseModel = new()
        {
            Model = result.AsReadOnly(),
            Parameters = request.Parameters,
            Pager = pager
        };

        return Result.Ok(responseModel);
    }
}