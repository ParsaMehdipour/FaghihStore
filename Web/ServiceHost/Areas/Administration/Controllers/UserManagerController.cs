using Identity.Infrastructure;
using Identity.Infrastructure.Services.Interfaces;

using Microsoft.AspNetCore.Identity;

using Role.Domain.Models;

using User.Application.Criteria;
using User.Application.Users.Commands.CreateUser;
using User.Application.Users.Commands.DeleteUser;
using User.Application.Users.Commands.EditUser;
using User.Application.Users.Queries.GetAllUsers;
using User.Application.Users.Queries.GetUser;

using ISmsService = SH.Application.Interfaces.ISmsService;

namespace ServiceHost.Areas.Administration.Controllers;

[Authorize(Policy = UserManagementPermissionExposer.Permissions.USER_FullManagement)]
public class UserManagerController : AdminController
{
    protected ISmsService _smsService { get; }
    protected IAuthManager _authManager { get; }
    protected RoleManager<ApplicationRole> _roleManager { get; }

    public UserManagerController(IMediator mediator,
        SH.Application.Interfaces.ISmsService smsService,
        IAuthManager authManager,
        RoleManager<ApplicationRole> roleManager) : base(mediator)
    {
        _smsService = smsService;
        _authManager = authManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index([FromQuery] UserQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "DisplayName");

        var response = await _mediator.Send(new GetAllUsersQuery(parameters), cancellationToken);

        return View(response.Value);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "DisplayName");

        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsFailed)
            return Json(new
            {
                isSuccess = false,
                messages = result.Errors.Select(_ => _.Message).ToList(),
            });

        if (command.SendVerificationCode)
        {
            var sendActivationCodeResult = await _smsService.SendActivationCodeAsync(command.PhoneNumber, cancellationToken);
            if (sendActivationCodeResult.IsFailed)
                return Json(new
                {
                    isSuccess = false,
                    messages = sendActivationCodeResult.Errors.Select(_ => _.Message).ToList(),
                });
        }

        return Json(result);
    }

    public async Task<IActionResult> Edit([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(new GetUserQuery(id), cancellationToken);

        ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "DisplayName", user.Value.Role);

        return PartialView("Edit", user.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserCommand command, CancellationToken cancellationToken)
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

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserCommand(id, false), cancellationToken);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserCommand(id, true), cancellationToken);

        return RedirectToAction("Index", new UserQueryStringParameters { IsDeleted = true });
    }

    public async Task<IActionResult> UpgradeRoleToAdmin(Guid id, CancellationToken cancellationToken)
    {
        var result = await _authManager.UpgradeUserRoleToAdmin(id, cancellationToken);
        if (result.IsFailed)
            return BadRequest(result.Errors.Select(_ => _.Message).FirstOrDefault());

        return RedirectToAction("Index");
    }
}
