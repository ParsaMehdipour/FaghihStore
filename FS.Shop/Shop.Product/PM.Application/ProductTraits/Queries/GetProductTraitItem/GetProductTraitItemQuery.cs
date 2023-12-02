using MediatR;

using PM.Application.ProductTraits.Commands.EditProductTraitItem;
using PM.Domain.ProductTraitAggregate;

namespace PM.Application.ProductTraits.Queries.GetProductTraitItem;

public record GetProductTraitItemQuery(Guid Id) : IRequest<Result<EditProductTraitItemCommand>>;

public static class ProductTraitItemExtension
{
    public static EditProductTraitItemCommand ToCommand(this ProductTraitItem productTraitItem) =>
        new(productTraitItem.Id, productTraitItem.Value, productTraitItem.OrderNumber,
            productTraitItem.HasInGeneralSpecification, productTraitItem.TraitId);
}