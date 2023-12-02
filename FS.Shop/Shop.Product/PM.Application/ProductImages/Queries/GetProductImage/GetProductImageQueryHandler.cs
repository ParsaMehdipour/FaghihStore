using MediatR;

using Microsoft.EntityFrameworkCore;

using PM.Domain.ProductImageAgg;

namespace PM.Application.ProductImages.Queries.GetProductImage;

public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQuery, Result<GetProductImageDto>>
{
    protected IProductImageRepository _productImageRepository { get; }

    public GetProductImageQueryHandler(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    public async Task<Result<GetProductImageDto>> Handle(GetProductImageQuery request, CancellationToken cancellationToken)
    {
        var productImage = await _productImageRepository.Get(pi => pi.Id.Equals(request.Id) && pi.ProductId.Equals(request.ProductId))
           .Select(_ => new GetProductImageDto(_.Id, _.ProductId, _.Title, _.Alt, _.GetFileNamePath())).FirstOrDefaultAsync(cancellationToken);

        ArgumentNullException.ThrowIfNull(productImage);

        return Result.Ok(productImage);
    }
}