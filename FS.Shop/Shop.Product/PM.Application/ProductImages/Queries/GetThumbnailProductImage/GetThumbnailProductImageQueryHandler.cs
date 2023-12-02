using MediatR;

using Microsoft.EntityFrameworkCore;

using PM.Application.ProductImages.Queries.GetProductImage;
using PM.Domain.ProductImageAgg;

namespace PM.Application.ProductImages.Queries.GetThumbnailProductImage;

public class GetThumbnailProductImageQueryHandler : IRequestHandler<GetThumbnailProductImageQuery, Result<GetProductImageDto>>
{
    protected IProductImageRepository _productImageRepository { get; }

    public GetThumbnailProductImageQueryHandler(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    public async Task<Result<GetProductImageDto>> Handle(GetThumbnailProductImageQuery request, CancellationToken cancellationToken)
    {
        var thumbnailImage = await _productImageRepository.Get(pi => pi.ProductId.Equals(request.ProductId) && pi.IsThumbnail)
            .Select(_ => new GetProductImageDto(_.Id, _.ProductId, _.Title, _.Alt, _.GetFileNamePath()))
            .FirstOrDefaultAsync(cancellationToken);

        ArgumentNullException.ThrowIfNull(thumbnailImage);

        return Result.Ok(thumbnailImage);
    }
}