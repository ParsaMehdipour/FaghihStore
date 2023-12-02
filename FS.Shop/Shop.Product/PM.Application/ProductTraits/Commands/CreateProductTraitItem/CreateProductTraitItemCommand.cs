using MediatR;

namespace PM.Application.ProductTraits.Commands.CreateProductTraitItem;

public record CreateProductTraitItemCommand(string Value, int OrderNumber, bool HasInGeneralSpecification, Guid ProductId, Guid TraitId) : IRequest<Result<Guid>>;