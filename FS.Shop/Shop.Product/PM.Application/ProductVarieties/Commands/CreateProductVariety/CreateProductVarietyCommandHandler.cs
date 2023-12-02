using MediatR;
using Microsoft.EntityFrameworkCore;
using PM.Domain.ProductAgg;
using PM.Domain.ProductVarietyAggregate;

namespace PM.Application.ProductVarieties.Commands.CreateProductVariety;

public class CreateProductVarietyCommandHandler : IRequestHandler<CreateProductVarietyCommand, Result<Guid>>
{
    protected IProductVarietyRepository _productVarietyRepository { get; }
    protected IProductRepository _productRepository { get; }
    protected ProductFactory _productFactory { get; }

    public CreateProductVarietyCommandHandler(IProductVarietyRepository productVarietyRepository, IProductRepository productRepository, ProductFactory productFactory)
    {
        _productVarietyRepository = productVarietyRepository;
        _productRepository = productRepository;
        _productFactory = productFactory;
    }

    public async Task<Result<Guid>> Handle(CreateProductVarietyCommand request, CancellationToken cancellationToken)
    {
        ProductVariety productVariety = _productFactory.CreateVariety(request.ProductId, request.VarietyId, request.InventoryId);
        Product product = await _productRepository.Get().Include(_ => _.ProductVarieties).FirstOrDefaultAsync(_ => _.Id == request.ProductId, cancellationToken);

        ArgumentNullException.ThrowIfNull(product, nameof(product));

        await _productVarietyRepository.AddAsync(productVariety, cancellationToken);
        await _productVarietyRepository.SaveAsync(cancellationToken);

        return Result.Ok(productVariety.Id);

    }
}
