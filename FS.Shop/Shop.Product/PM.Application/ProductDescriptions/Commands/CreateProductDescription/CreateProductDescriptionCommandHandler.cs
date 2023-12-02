using MediatR;
using PM.Domain.ProductAgg;
using PM.Domain.ProductDescriptionAgg;

namespace PM.Application.ProductDescriptions.Commands.CreateProductDescription;

public class CreateProductDescriptionCommandHandler : IRequestHandler<CreateProductDescriptionCommand, Result<Guid>>
{
    protected IProductDescriptionRepository _productDescriptionRepository { get; }
    protected ProductFactory _productFactory { get; }


    public CreateProductDescriptionCommandHandler(IProductDescriptionRepository productDescriptionRepository, ProductFactory productFactory)
    {
        _productDescriptionRepository = productDescriptionRepository;
        _productFactory = productFactory;
    }

    public async Task<Result<Guid>> Handle(CreateProductDescriptionCommand request, CancellationToken cancellationToken)
    {
        ProductDescription productDescription = _productFactory.CreateDescription(request.Title, request.Description, request.ProductId);

        await _productDescriptionRepository.AddAsync(productDescription, cancellationToken);
        await _productDescriptionRepository.SaveAsync(cancellationToken);

        return Result.Ok(productDescription.Id);
    }
}