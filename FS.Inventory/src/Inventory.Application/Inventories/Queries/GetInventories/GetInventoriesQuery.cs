using FluentResults;
using Inventory.Application.Criteria;
using MediatR;
using SH.Infrastructure.Criteria;

namespace Inventory.Application.Inventories.Queries.GetInventories;

public record GetInventoriesQuery(InventoryQueryStringParameters Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetInventoriesDto>, InventoryQueryStringParameters>>>;

public record GetInventoriesDto(Guid Id, string Product, string ProductVariety, string UnitPrice, bool InStock,
    long CurrentCount, string CreateDate)
{
    public string Product { get; set; }
    public string ProductVariety { get; set; }
}