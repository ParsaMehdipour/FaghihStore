using FluentResults;

using Inventory.Application.Models;
using Inventory.Domain.Models;
using Inventory.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Options;

using SH.Infrastructure.Services;
using SH.Infrastructure.Settings;

namespace Inventory.Application.Inventories.Commands.CreateInventory;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Result<Guid>>
{
    protected IInventoryRepository _inventoryRepository { get; }
    protected HttpClientService _httpClientService { get; }
    protected InventoryFactory _inventoryFactory { get; }
    protected List<ProjectsUrls> ProjectsUrls { get; }

    public CreateInventoryCommandHandler(IInventoryRepository invetoryRepository,
        HttpClientService httpClientService,
        InventoryFactory inventoryFactory,
        IOptions<SiteSettings> options)
    {
        _inventoryRepository = invetoryRepository;
        _httpClientService = httpClientService;
        _inventoryFactory = inventoryFactory;
        ProjectsUrls = options.Value.ProjectsUrls;
    }

    public async Task<Result<Guid>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        Domain.Models.Inventory inventory = _inventoryFactory.Create(Guid.Empty, request.UnitPrice, request.Count);

        await _inventoryRepository.AddAsync(inventory, cancellationToken);
        await _inventoryRepository.SaveAsync(cancellationToken);

        _httpClientService.SetBaseAddress(ProjectsUrls.FirstOrDefault(_ => _.Project.Equals("PM.Api")).Url);

        CreateProductVarietyDto requestPost = new(request.ProductId, request.VarietyId, inventory.Id);

        var productVarietyId = await _httpClientService.Post<CreateProductVarietyDto, Guid>(requestPost, "api/ProductVariety/CreateProductVariety/", cancellationToken);

        inventory.SetProductVarietyId(productVarietyId);

        await _inventoryRepository.SaveAsync(cancellationToken);

        return Result.Ok(inventory.Id);
    }
}
