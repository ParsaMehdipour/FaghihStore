using MediatR;

using Microsoft.EntityFrameworkCore;

using PM.Domain.ProductImageAgg;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Extensions;

namespace PM.Application.ProductImages.Queries.GetProductImages;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQuery, Result<ResponseModel<GetProductImagesViewModel>>>
{
    public IProductImageRepository _productImageRepository { get; set; }

    public GetProductImagesQueryHandler(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    public async Task<Result<ResponseModel<GetProductImagesViewModel>>> Handle(GetProductImagesQuery request, CancellationToken cancellationToken)
    {
        var productImages = await _productImageRepository.Get(_ => _.ProductId.Equals(request.ProductId) && _.IsDeleted == request.Parameters.IsDeleted)
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Include(_ => _.Product)
            .OrderByDescending(_ => _.CreatedDate)
            .ToListAsync(cancellationToken);

        var result = productImages.Select(_ => new GetProductImagesDto(_.Id, _.GetFileNamePath(), _.Alt, _.Title, _.CreatedDate.ToPersian(), _.IsThumbnail)).ToList();

        var product = productImages.Select(_ => _.Product).FirstOrDefault();

        ResponseModel<GetProductImagesViewModel> response = new()
        {
            Model = new()
            {
                Images = result.AsReadOnly(),
                ProductId = product.Id,
                ProductTitle = product.TitlePersian
            }
        };

        return Result.Ok(response);
    }
}