using Category.Application.Categories.Queries.GetCategories;

using TG.Application.Criteria;
using TG.Application.TraitGroups.Queries.GetTraitGroups;
using TG.Application.Traits.Commands.CreateTrait;
using TG.Application.Traits.Commands.DeleteTrait;
using TG.Application.Traits.Commands.EditTrait;
using TG.Application.Traits.Queries.GetTrait;
using TG.Application.Traits.Queries.GetTraits;
using TG.Infrastructure.EfCore;

namespace ServiceHost.Areas.Administration.Controllers;

public class TraitController : AdminController
{
    public TraitController(IMediator mediator) : base(mediator)
    {

    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAIT_View)]
    public async Task<IActionResult> Index([FromQuery] TraitQueryStringParameter parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request: new GetTraitsQuery(parameters), cancellationToken);

        if (response.IsFailed)
            return NotFound("we have problem. please contact us!");

        return View(response.Value);
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAIT_Create)]
    public async Task<IActionResult> Create()
    {
        var traitGroups = await _mediator.Send(new GetTraitGroupsQuery(new TraitQueryStringParameter()));
        var categories = await _mediator.Send(new GetCategoriesQuery(new()));

        ViewBag.TraitGroups = new SelectList(traitGroups.Value.Model, "Id", "Title");
        ViewBag.Categories = new SelectList(categories.Value.Model, "Id", "Title");

        return PartialView();
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAIT_Create)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTraitCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: command, cancellationToken);

        if (result.IsFailed)
            return Json(new
            {
                isSuccess = false,
                messages = result.Errors.Select(_ => _.Message).ToList(),
            });

        return Json(result);
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAIT_Edit)]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTraitQuery(id), cancellationToken);

        if (result.IsFailed)
            return Json(new
            {
                isSuccess = false,
                messages = result.Errors.Select(_ => _.Message).ToList(),
            });

        var traitGroups = await _mediator.Send(new GetTraitGroupsQuery(new TraitQueryStringParameter()));
        var categories = await _mediator.Send(new GetCategoriesQuery(new()));

        ViewBag.TraitGroups = new SelectList(traitGroups.Value.Model, "Id", "Title", result.Value.TraitGroupId);
        ViewBag.Categories = new SelectList(categories.Value.Model, "Id", "Title", result.Value.CategoryId);

        return PartialView(result.Value);
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAIT_Delete)]
    [HttpPost]
    public async Task<IActionResult> Edit(EditTraitCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: command, cancellationToken);

        if (result.IsFailed)
        {
            if (result.IsFailed)
                return Json(new
                {
                    isSuccess = false,
                    messages = result.Errors.Select(_ => _.Message).ToList(),
                });
        }

        return Json(result);
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAIT_Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteTraitCommand(Id: id, IsRestored: false), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAIT_Delete)]
    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteTraitCommand(Id: id, IsRestored: true), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }
}