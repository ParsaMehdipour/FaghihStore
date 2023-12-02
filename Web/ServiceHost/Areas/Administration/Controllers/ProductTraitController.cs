using PM.Application.Criteria;
using PM.Application.ProductTraits.Commands.CreateProductTraitItem;
using PM.Application.ProductTraits.Commands.EditProductTraitItem;
using PM.Application.ProductTraits.Queries.GetProductTraitItem;
using PM.Application.ProductTraits.Queries.GetProductTraitItems;
using PM.Infrastructure.EFCore;

using TG.Application.Traits.Queries.GetTraitsContainTraitGroups;

using VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

namespace ServiceHost.Areas.Administration.Controllers;

public class ProductTraitController : AdminController
{
    public ProductTraitController(IMediator mediator) : base(mediator)
    {
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTTRAIT_View)]
    public async Task<IActionResult> Index(Guid productId, ProductTraitQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        if (productId == Guid.Empty)
            return NotFound();

        var productTraitItems = await _mediator.Send(new GetProductTraitItemsQuery(productId, parameters), cancellationToken);

        if (productTraitItems.IsFailed)
            return Json(new
            {
                isSuccess = false,
                messages = productTraitItems.Errors.Select(_ => _.Message).ToList(),
            });

        return View(productTraitItems.Value);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTTRAIT_Create)]
    public async Task<IActionResult> Create(Guid productId, CancellationToken cancellationToken)
    {
        CreateProductTraitItemCommand command = new(string.Empty, 0, false, productId, Guid.Empty);

        var traits = await _mediator.Send(new GetTraitsContainTraitGroupsQuery(), cancellationToken);

        ViewBag.Traits = new SelectList(traits.Value.Model.Select(_ => new
        {
            Id = _.Id,
            Trait = _.Trait + $"({_.TraitGroup})"
        }), "Id", "Trait");

        return PartialView(command);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTTRAIT_Create)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductTraitItemCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTTRAIT_Edit)]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProductTraitItemQuery(id));

        var traits = await _mediator.Send(new GetTraitsContainTraitGroupsQuery(), cancellationToken);

        ViewBag.Traits = new SelectList(traits.Value.Model.Select(_ => new
        {
            Id = _.Id,
            Trait = _.Trait + $"({_.TraitGroup})"
        }), "Id", "Trait", result.Value.TraitId);

        return PartialView(result.Value);
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTTRAIT_Edit)]
    [HttpPost]
    public async Task<IActionResult> Edit(EditProductTraitItemCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTTRAIT_Delete)]
    public async Task<IActionResult> Delete(Guid id, Guid productId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteProductTraitItemCommand(id, false), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index", new { productId });
    }

    [Authorize(Policy = ShopManagementPermissionExposer.Permissions.PRODUCTTRAIT_Delete)]
    public async Task<IActionResult> Restore(Guid id, Guid productId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteProductTraitItemCommand(id, true), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index", new { productId });
    }
}