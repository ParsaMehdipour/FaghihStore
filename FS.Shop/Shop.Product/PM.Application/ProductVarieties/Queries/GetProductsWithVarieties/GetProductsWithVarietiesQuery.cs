using MediatR;

namespace PM.Application.ProductVarieties.Queries.GetProductsWithVarieties;

public record GetProductsWithVarietiesQuery(Dictionary<Guid, Guid> ProductVarietiesDictionary) : IRequest<Result<List<GetProductsWithVarietiesDto>>>;

public record GetProductsWithVarietiesDto(Guid InventoryId, string ProductTitle, string VarietyTitle);