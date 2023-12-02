using MediatR;

namespace PM.Application.ProductTraits.Commands.EditProductTraitItem;

public record EditProductTraitItemCommand(Guid Id, string Value, int OrderNumber, bool HasInGeneralSpecification, Guid TraitId) : IRequest<Result<Guid>>;