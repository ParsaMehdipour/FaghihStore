using FluentResults;

using Inventory.Application.Criteria;
using Inventory.Application.Models;
using Inventory.Domain.Repositories;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;
using SH.Infrastructure.Services;
using SH.Infrastructure.Settings;

namespace Inventory.Application.Inventories.Queries.GetInventories;

public class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, Result<ResponseModel<IEnumerable<GetInventoriesDto>, InventoryQueryStringParameters>>>
{
    protected IInventoryRepository _inventoryRepository { get; }
    protected HttpClientService _httpClientService { get; }
    protected List<ProjectsUrls> ProjectsUrls { get; }

    public GetInventoriesQueryHandler(IInventoryRepository inventoryRepository,
        HttpClientService httpClientService,
        IOptions<SiteSettings> options)
    {
        _inventoryRepository = inventoryRepository;
        _httpClientService = httpClientService;
        ProjectsUrls = options.Value.ProjectsUrls;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetInventoriesDto>, InventoryQueryStringParameters>>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
    {
        var inventories = _inventoryRepository.Get().IgnoreQueryFilters().Where(_ => _.IsDeleted == request.Parameters.IsDeleted || _.InStock == request.Parameters.InStock);

        _httpClientService.SetBaseAddress(ProjectsUrls.FirstOrDefault(_ => _.Project.Equals("PM.Api")).Url);
        var inventoriesWithProductAndVarieties = await _httpClientService.Send<List<GetProductsWithVarietiesDto>>("api/ProductVariety/GetProductVarietiesWithInventories/" + request.Parameters.ProductId, cancellationToken);

        int count = await inventories.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        var result = await inventories.Where(a => inventoriesWithProductAndVarieties.Select(b => b.InventoryId).Contains(a.Id)).OrderByDescending(_ => _.CreatedDate).Paginate(pager)
            .Select(_ => new GetInventoriesDto(
                    _.Id,
                    string.Empty,
                    string.Empty,
                    _.UnitPrice.ToString("n0"),
                    _.InStock,
                    _.CalculateCurrentCount(),
                 _.CreatedDate.ToPersianDate()
                )
            ).ToListAsync(cancellationToken);

        result.ForEach(_ =>
        {

            _.Product = inventoriesWithProductAndVarieties.FirstOrDefault(i => i.InventoryId == _.Id)?.ProductTitle;
            _.ProductVariety = inventoriesWithProductAndVarieties.FirstOrDefault(i => i.InventoryId == _.Id)?.VarietyTitle;
        });


        ResponseModel<IEnumerable<GetInventoriesDto>, InventoryQueryStringParameters> responseModel = new()
        {
            Model = result.AsReadOnly(),
            Parameters = request.Parameters,
            Pager = pager
        };

        return Result.Ok(responseModel);
    }
}

