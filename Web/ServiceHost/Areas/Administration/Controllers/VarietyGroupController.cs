using VG.Application.Criteria;
using VG.Application.VarietyGroups.Commands.CreateVarietyGroup;
using VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;
using VG.Application.VarietyGroups.Commands.EditVarietyGroup;
using VG.Application.VarietyGroups.Queries.GetVarietyGroup;
using VG.Application.VarietyGroups.Queries.GetVarietyGroups;
using VG.Infrastructure.EfCore;

namespace ServiceHost.Areas.Administration.Controllers;
public class VarietyGroupController : AdminController
{

    public VarietyGroupController(IMediator mediator) : base(mediator)
    {

    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETYGROUP_View)]
    public async Task<IActionResult> Index([FromQuery] VarietyGroupQueryStringParameter parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request: new GetVarietyGroupsQuery(parameters), cancellationToken);

        return View(response.Value);
    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETYGROUP_Create)]
    public IActionResult Create()
    {
        return PartialView("Create");
    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETYGROUP_Create)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateVarietyGroupCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETYGROUP_Edit)]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var varietyGroup = await _mediator.Send(request: new GetVarietyGroupQuery(id), cancellationToken);

        return PartialView("Edit", varietyGroup.Value);
    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETYGROUP_Edit)]
    [HttpPost]
    public async Task<IActionResult> Edit(EditVarietyGroupCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETYGROUP_Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteVarietyGroupCommand(Id: id, IsRestored: false), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETYGROUP_Delete)]
    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteVarietyGroupCommand(Id: id, IsRestored: true), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }

}
