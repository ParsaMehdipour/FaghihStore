using MediatR;
using PM.Domain.ProductDescriptionAgg;

namespace PM.Application.ProductDescriptions.Commands.EditProductDescription;

public class EditProductDescriptionCommandHandler : IRequestHandler<EditProductDescriptionCommand, Result>
{

    protected IProductDescriptionRepository _productDescriptionRepository { get; }

    public EditProductDescriptionCommandHandler(IProductDescriptionRepository productDescriptionRepository)
    {
        _productDescriptionRepository = productDescriptionRepository;
    }

    public async Task<Result> Handle(EditProductDescriptionCommand request, CancellationToken cancellationToken)
    {
        ProductDescription productDescription = await _productDescriptionRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(productDescription, nameof(productDescription));

        productDescription.SetTitle(request.Title);
        productDescription.SetDescription(request.Description);
        productDescription.SetProductId(request.ProductId);

        _productDescriptionRepository.Update(productDescription);
        await _productDescriptionRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
