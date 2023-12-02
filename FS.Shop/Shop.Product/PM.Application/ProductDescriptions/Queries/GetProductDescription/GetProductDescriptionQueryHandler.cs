using MediatR;
using PM.Application.ProductDescriptions.Commands.EditProductDescription;
using PM.Domain.ProductDescriptionAgg;

namespace PM.Application.ProductDescriptions.Queries.GetProductDescription;

public class GetProductDescriptionQueryHandler : IRequestHandler<GetProductDescriptionQuery, Result<EditProductDescriptionCommand>>
{
    public GetProductDescriptionQueryHandler(IProductDescriptionRepository productDescriptionRepository)
    {
        _productDescriptionRepository = productDescriptionRepository;
    }

    protected IProductDescriptionRepository _productDescriptionRepository { get; }

    public async Task<Result<EditProductDescriptionCommand>> Handle(GetProductDescriptionQuery request, CancellationToken cancellationToken)
    {
        var productDescription = await _productDescriptionRepository.GetWithoutQueryFilterAsync(_ => _.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(productDescription, nameof(productDescription));

        var productDescriptionDto = new EditProductDescriptionCommand(Id: productDescription.Id, ProductId: productDescription.ProductId, Title: productDescription.Title, Description: productDescription.Description);

        return Result.Ok(productDescriptionDto);
    }
}
