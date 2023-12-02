using MediatR;

namespace PM.Application.ProductVarieties.Commands.CreateProductVariety;

public record CreateProductVarietyCommand(Guid ProductId, Guid VarietyId, Guid InventoryId) : IRequest<Result<Guid>>;