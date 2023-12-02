using MediatR;

using PM.Application.Products.Commands.EditProduct;
using PM.Domain.ProductAgg;

namespace PM.Application.Products.Queries.GetProduct;
public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<EditProductCommand>>
{
    protected IProductRepository _productRepository { get; }

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<EditProductCommand>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetIncludeThumbnailImageAsync(request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(product, nameof(product));

        var editProductCommand = new EditProductCommand(product.Id, product.TitlePersian, product.TitleEnglish, product.Slug,
            product.MetaDescription, product.PublishedDate, product.WarrantyDescription,
            product.CategoryId, product.BrandId, product.Images.FirstOrDefault().ToCommand());

        return Result.Ok(editProductCommand);
    }
}