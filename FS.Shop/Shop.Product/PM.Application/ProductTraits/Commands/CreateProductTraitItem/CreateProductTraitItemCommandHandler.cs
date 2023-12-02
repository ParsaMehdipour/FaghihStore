using MediatR;

using PM.Domain.ProductTraitAggregate;

namespace PM.Application.ProductTraits.Commands.CreateProductTraitItem;

public class CreateProductTraitItemCommandHandler : IRequestHandler<CreateProductTraitItemCommand, Result<Guid>>
{
    protected IProductTraitItemRepository _productTraitItemRepository { get; }
    protected ProductTraitItemFactory _factory { get; }

    public CreateProductTraitItemCommandHandler(IProductTraitItemRepository productTraitItemRepository,
        ProductTraitItemFactory factory)
    {
        _productTraitItemRepository = productTraitItemRepository;
        _factory = factory;
    }

    public async Task<Result<Guid>> Handle(CreateProductTraitItemCommand request, CancellationToken cancellationToken)
    {
        var productTraitItem = _factory.Create(request.Value, request.OrderNumber, request.HasInGeneralSpecification, request.ProductId, request.TraitId);

        await _productTraitItemRepository.AddAsync(productTraitItem, cancellationToken);
        await _productTraitItemRepository.SaveAsync(cancellationToken);

        return Result.Ok(productTraitItem.Id);
    }
}