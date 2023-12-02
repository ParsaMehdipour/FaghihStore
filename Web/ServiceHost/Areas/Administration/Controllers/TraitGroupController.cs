using TG.Application.Criteria;
using TG.Application.TraitGroups.Commands.CreateTraitGroup;
using TG.Application.TraitGroups.Commands.EditTraitGroup;
using TG.Application.TraitGroups.Queries.GetTraitGroup;
using TG.Application.TraitGroups.Queries.GetTraitGroups;
using TG.Infrastructure.EfCore;

using VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

namespace ServiceHost.Areas.Administration.Controllers;

public class TraitGroupController : AdminController
{
    public TraitGroupController(IMediator mediator) : base(mediator)
    {

    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAITGROUP_View)]
    public async Task<IActionResult> Index([FromQuery] TraitGroupQueryStringParameter parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request: new GetTraitGroupsQuery(parameters), cancellationToken);

        return View(response.Value);
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAITGROUP_Create)]
    public IActionResult Create()
    {
        return PartialView();
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAITGROUP_Create)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTraitGroupCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAITGROUP_Edit)]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var traitGroup = await _mediator.Send(new GetTraitGroupQuery(id), cancellationToken);

        return PartialView(traitGroup.Value);
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAITGROUP_Edit)]
    [HttpPost]
    public async Task<IActionResult> Edit(EditTraitGroupCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAITGROUP_Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteTraitGroupCommand(Id: id, IsRestored: false), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }

    [Authorize(Policy = TraitGroupManagementPermissionExposer.Permissions.TRAITGROUP_Delete)]
    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteTraitGroupCommand(Id: id, IsRestored: true), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }
}