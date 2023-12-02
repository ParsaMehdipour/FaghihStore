using VG.Application.Criteria;
using VG.Application.Varieties.Commands.CreateVariety;
using VG.Application.Varieties.Commands.DeleteVariety;
using VG.Application.Varieties.Commands.EditVariety;
using VG.Application.Varieties.Queries.GetVarieties;
using VG.Application.Varieties.Queries.GetVariety;
using VG.Application.VarietyGroups.Queries.GetVarietyGroupsForVariety;
using VG.Domain.Enums;
using VG.Infrastructure.EfCore;

namespace ServiceHost.Areas.Administration.Controllers;

public class VarietyController : AdminController
{

    public VarietyController(IMediator mediator) : base(mediator)
    {

    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETY_View)]
    public async Task<IActionResult> Index([FromQuery] VarietyQueryStringParameter parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request: new GetVarietiesQuery(parameters), cancellationToken);

        return View(response.Value);
    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETY_Create)]
    public async Task<IActionResult> Create()
    {
        var response = await _mediator.Send(new GetVarietyGroupsForVarietyQuery());
        ViewBag.VarietyGroups = new SelectList(response.Value, "Id", "Title");
        ViewBag.BoxTypes = Enum.GetValues(typeof(BoxType)).Cast<BoxType>().Select(v => new SelectListItem
        {
            Text = v.ToString(),
            Value = ((int)v).ToString()
        }).ToList();

        return PartialView("Create");
    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETY_Create)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateVarietyCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETY_Edit)]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var variety = await _mediator.Send(request: new GetVarietyQuery(id), cancellationToken);
        var response = await _mediator.Send(new GetVarietyGroupsForVarietyQuery());
        ViewBag.VarietyGroups = new SelectList(response.Value, "Id", "Title");
        ViewBag.BoxTypes = Enum.GetValues(typeof(BoxType)).Cast<BoxType>().Select(v => new SelectListItem
        {
            Text = v.ToString(),
            Value = ((int)v).ToString()
        }).ToList();

        return PartialView("Edit", variety.Value);
    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETY_Edit)]
    [HttpPost]
    public async Task<IActionResult> Edit(EditVarietyCommand command, CancellationToken cancellationToken)
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

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETY_Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteVarietyCommand(Id: id, IsRestored: false), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }

    [Authorize(Policy = VarietyGroupManagementPermissionExposer.Permissions.VARIETY_Delete)]
    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request: new DeleteVarietyCommand(Id: id, IsRestored: true), cancellationToken);

        if (result.IsFailed)
        {
            //TODO: should be there a toastify or sweetAlert message for show
            return NotFound(result.Errors.First()); //TODO: this code is temporary
        }

        return RedirectToAction("Index");
    }
}
