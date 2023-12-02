using MediatR;

using PM.Domain.ProductTraitAggregate;

namespace VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

public class DeleteProductTraitItemCommandHandler : IRequestHandler<DeleteProductTraitItemCommand, Result>
{
    protected IProductTraitItemRepository _productTraitItemRepository { get; }

    public DeleteProductTraitItemCommandHandler(IProductTraitItemRepository productTraitItemRepository)
    {
        _productTraitItemRepository = productTraitItemRepository;
    }

    public async Task<Result> Handle(DeleteProductTraitItemCommand request, CancellationToken cancellationToken)
    {
        ProductTraitItem productTraitItem = await _productTraitItemRepository.GetWithoutQueryFilterAsync(_ => _.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(productTraitItem, paramName: nameof(productTraitItem));

        if (request.IsRestored is true) productTraitItem.RestoreItem();
        else productTraitItem.DeleteItem();

        await _productTraitItemRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}