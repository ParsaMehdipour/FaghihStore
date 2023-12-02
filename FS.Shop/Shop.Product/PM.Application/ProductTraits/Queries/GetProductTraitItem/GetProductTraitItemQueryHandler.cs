using MediatR;

using PM.Application.ProductTraits.Commands.EditProductTraitItem;
using PM.Domain.ProductTraitAggregate;

namespace PM.Application.ProductTraits.Queries.GetProductTraitItem;

public class GetProductTraitItemQueryHandler : IRequestHandler<GetProductTraitItemQuery, Result<EditProductTraitItemCommand>>
{
    protected IProductTraitItemRepository _productTraitItemRepository { get; }

    public GetProductTraitItemQueryHandler(IProductTraitItemRepository productTraitItemRepository)
    {
        _productTraitItemRepository = productTraitItemRepository;
    }

    public async Task<Result<EditProductTraitItemCommand>> Handle(GetProductTraitItemQuery request, CancellationToken cancellationToken)
    {
        var productTraitItem = await _productTraitItemRepository.GetByIdAsync(cancellationToken, request.Id);

        return Result.Ok(productTraitItem.ToCommand());
    }
}