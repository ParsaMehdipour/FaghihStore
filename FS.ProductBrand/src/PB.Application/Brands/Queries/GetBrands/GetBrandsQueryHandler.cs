using MediatR;
using Microsoft.EntityFrameworkCore;
using PB.Application.Criteria;
using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;

namespace PB.Application.Brands.Queries.GetBrands;

public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, Result<ResponseModel<IEnumerable<GetBrandDto>, BrandQueryStringParameters>>>
{
    protected IBrandRepository _brandRepository { get; }

    public GetBrandsQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetBrandDto>, BrandQueryStringParameters>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = _brandRepository.Get();

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            brands = brands.Where(_ => _.Title.Contains(request.Parameters.Search));

        int count = await brands.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        if (request.Parameters.IsDeleted == true)
            brands = brands.IgnoreQueryFilters().Where(_ => _.IsDeleted == true);

        var result = await brands.Select(_ => new GetBrandDto(
            _.Id,
            _.Title,
            _.Slug,
            _.Status,
            _.IsDeleted,
            _.CreatedDate.ToPersian())
        ).ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetBrandDto>, BrandQueryStringParameters> response = new()
        {
            Model = result.AsReadOnly(),
            Pager = pager,
            Parameters = request.Parameters
        };

        return Result.Ok(response);
    }
}
