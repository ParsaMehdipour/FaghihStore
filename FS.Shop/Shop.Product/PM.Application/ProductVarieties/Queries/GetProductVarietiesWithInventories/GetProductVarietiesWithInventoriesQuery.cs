using MediatR;

namespace PM.Application.ProductVarieties.Queries.GetProductVarietiesWithInventories;

public record GetProductVarietiesWithInventoriesQuery(Guid ProductId) : IRequest<Result<List<GetProductVarietiesWithInventoriesDto>>>;

public record GetProductVarietiesWithInventoriesDto(Guid InventoryId, string ProductTitle, string VarietyTitle);
