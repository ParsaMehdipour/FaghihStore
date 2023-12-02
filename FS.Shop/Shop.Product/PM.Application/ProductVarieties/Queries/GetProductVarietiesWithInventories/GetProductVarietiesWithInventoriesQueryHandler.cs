using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using PM.Domain.ProductVarietyAggregate;

using SH.Infrastructure.Services;
using SH.Infrastructure.Settings;

namespace PM.Application.ProductVarieties.Queries.GetProductVarietiesWithInventories;

public class GetProductVarietiesWithInventoriesQueryHandler : IRequestHandler<GetProductVarietiesWithInventoriesQuery, Result<List<GetProductVarietiesWithInventoriesDto>>>
{
    protected IProductVarietyRepository _productVarietyRepository { get; }
    protected HttpClientService _httpClientService { get; }
    protected List<ProjectsUrls> ProjectsUrls { get; }

    public GetProductVarietiesWithInventoriesQueryHandler(IProductVarietyRepository productVarietyRepository,
        HttpClientService httpClientService,
        IOptions<SiteSettings> options)
    {
        _productVarietyRepository = productVarietyRepository;
        _httpClientService = httpClientService;
        ProjectsUrls = options.Value.ProjectsUrls;
    }

    public async Task<Result<List<GetProductVarietiesWithInventoriesDto>>> Handle(GetProductVarietiesWithInventoriesQuery request, CancellationToken cancellationToken)
    {
        List<GetProductVarietiesWithInventoriesDto> result = new();

        var productVarieties = await _productVarietyRepository.Get(_ => _.ProductId == request.ProductId).Include(_ => _.Product).ToListAsync(cancellationToken);

        //Key = InventoryId , Value = VarietyId
        var InventoriesWithVarietiesDictionary = productVarieties.ToDictionary(_ => _.InventoryId, _ => _.VarietyId);

        _httpClientService.SetBaseAddress(ProjectsUrls.FirstOrDefault(_ => _.Project.Equals("VG.Api")).Url);
        var varieties = await _httpClientService.Send<Dictionary<Guid, Guid>, Dictionary<Guid, string>>(InventoriesWithVarietiesDictionary, "api/Variety/GetInventoriesVarieties/", cancellationToken);

        foreach (var productVariety in productVarieties)
        {
            GetProductVarietiesWithInventoriesDto resultDto = new(productVariety.InventoryId, productVariety.Product.TitlePersian, varieties[productVariety.InventoryId]);

            result.Add(resultDto);
        }

        return Result.Ok(result);
    }
}
