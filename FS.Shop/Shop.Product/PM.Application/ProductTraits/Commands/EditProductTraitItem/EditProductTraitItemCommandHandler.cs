using MediatR;

using PM.Domain.ProductTraitAggregate;

namespace PM.Application.ProductTraits.Commands.EditProductTraitItem;

public class EditProductTraitItemCommandHandler : IRequestHandler<EditProductTraitItemCommand, Result<Guid>>
{
    protected IProductTraitItemRepository _productTraitItemRepository { get; }

    public EditProductTraitItemCommandHandler(IProductTraitItemRepository productTraitItemRepository)
    {
        _productTraitItemRepository = productTraitItemRepository;
    }

    public async Task<Result<Guid>> Handle(EditProductTraitItemCommand request, CancellationToken cancellationToken)
    {
        var productTraitItem = await _productTraitItemRepository.GetByIdAsync(cancellationToken, request.Id);

        productTraitItem.SetValue(request.Value);
        productTraitItem.SetTraitId(request.TraitId);
        productTraitItem.SetHasInGeneralSpecification(request.HasInGeneralSpecification);
        productTraitItem.SetOrderNumber(request.OrderNumber);
        productTraitItem.SetModifiedDate(DateTime.Now);

        await _productTraitItemRepository.SaveAsync(cancellationToken);

        return Result.Ok(productTraitItem.Id);
    }
}