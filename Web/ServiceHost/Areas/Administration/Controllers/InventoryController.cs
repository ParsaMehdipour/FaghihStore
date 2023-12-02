using Inventory.Application.Criteria;
using Inventory.Application.Inventories.Commands.CreateInventory;
using Inventory.Application.Inventories.Commands.CreateInventoryOperation;
using Inventory.Application.Inventories.Commands.EditInventory;
using Inventory.Application.Inventories.Queries.GetInventories;
using Inventory.Application.Inventories.Queries.GetInventory;
using Inventory.Application.Inventories.Queries.GetInventoryOperationLogs;
using Inventory.Domain.Enums;
using Inventory.Infrastructure.EFCore;

using PM.Application.Products.Queries.GetProducts;

using VG.Application.Varieties.Queries.GetVarieties;

namespace ServiceHost.Areas.Administration.Controllers;

public class InventoryController : AdminController
{
    public InventoryController(IMediator mediator) : base(mediator)
    {

    }

    [Authorize(Policy = InventoryManagementPermissionExposer.Permissions.INVENTORY_View)]
    public async Task<IActionResult> Index([FromQuery] InventoryQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetInventoriesQuery(parameters), cancellationToken);

        return View(response.Value);
    }

    [Authorize(Policy = InventoryManagementPermissionExposer.Permissions.INVENTORY_Create)]
    public async Task<IActionResult> Create(Guid productId, CancellationToken cancellationToken)
    {
        CreateInventoryCommand command = new(Guid.Empty, productId, 0, 0);

        var varietyResponse = await _mediator.Send(new GetVarietiesQuery(new()), cancellationToken);
        ViewBag.Varieties = new SelectList(varietyResponse.Value.Model, "Id", "Title");

        return PartialView(command);
    }

    [Authorize(Policy = InventoryManagementPermissionExposer.Permissions.INVENTORY_Create)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateInventoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
        {
            var productsResponse = await _mediator.Send(new GetProductsQuery(new()), cancellationToken);
            ViewBag.Products = new SelectList(productsResponse.Value.Model, "Id", "Title");

            var varietyResponse = await _mediator.Send(new GetVarietiesQuery(new()), cancellationToken);
            ViewBag.Varieties = new SelectList(varietyResponse.Value.Model, "Id", "Title");

            return Json(new
            {
                isSuccess = false,
                messages = result.Errors.Select(_ => _.Message).ToList(),
            });
        }

        return Json(result);
    }

    [Authorize(Policy = InventoryManagementPermissionExposer.Permissions.INVENTORY_Edit)]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetInventoryQuery(id), cancellationToken);

        return PartialView(response.Value);
    }

    [Authorize(Policy = InventoryManagementPermissionExposer.Permissions.INVENTORY_Edit)]
    [HttpPost]
    public async Task<IActionResult> Edit(EditInventoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
        {
            return Json(new
            {
                isSuccess = false,
                messages = result.Errors.Select(_ => _.Message).ToList(),
            });
        }

        return Json(result);
    }

    [Authorize(Policy = InventoryManagementPermissionExposer.Permissions.INVENTORY_Increase)]
    public IActionResult Increase(Guid id)
    {
        CreateInventoryOperationCommand command = new(id, InventoryOperationType.Increased, 0, string.Empty);

        return PartialView(command);
    }

    [Authorize(Policy = InventoryManagementPermissionExposer.Permissions.INVENTORY_Reduce)]
    public IActionResult Reduce(Guid id)
    {
        CreateInventoryOperationCommand command = new(id, InventoryOperationType.Reduce, 0, string.Empty);

        return PartialView(command);
    }

    [HttpPost]
    public async Task<IActionResult> Operate(CreateInventoryOperationCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
            return Json(new
            {
                isSuccess = false,
                messages = result.Errors.Select(_ => _.Message).ToList(),
            });

        return Json(result);
    }

    [Authorize(Policy = InventoryManagementPermissionExposer.Permissions.INVENTORYOPERATION_Logs)]
    public async Task<IActionResult> OperationLogs(Guid inventoryId, InventoryOperationQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetInventoryOperationLogsQuery(inventoryId, parameters), cancellationToken);

        return View(response.Value);
    }
}