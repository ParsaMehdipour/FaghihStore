using Identity.Infrastructure;

using Role.Application.Criteria;
using Role.Application.Roles.Commands.CreateRole;
using Role.Application.Roles.Commands.DeleteRole;
using Role.Application.Roles.Commands.EditRole;
using Role.Application.Roles.Queries.GetRole;
using Role.Application.Roles.Queries.GetRoles;

namespace ServiceHost.Areas.Administration.Controllers;

[Authorize(Policy = UserManagementPermissionExposer.Permissions.ROLE_FullManagement)]
public class RoleController : AdminController
{
    public RoleController(IMediator mediator) : base(mediator)
    {

    }

    public async Task<IActionResult> Index([FromQuery] RoleQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetRolesQuery(parameters), cancellationToken);

        return View(response.Value);
    }

    public IActionResult Create()
    {
        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleCommand command, CancellationToken cancellationToken)
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

    //[Authorize(Policy = UserManagementPermissionExposer.Permissions.ROLE_Edit)]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var role = await _mediator.Send(new GetRoleQuery(id), cancellationToken);

        return View(role.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
        {
            ModelState.AddModelError(string.Empty, result.Errors.Select(_ => _.Message).First());
            return View(command);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteRoleCommand(id, false), cancellationToken);

        if (result.IsFailed)
        {
            return NotFound(result.Errors.First());
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteRoleCommand(id, true), cancellationToken);

        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("error", error.Message);
        }

        return View("Index");
    }
}