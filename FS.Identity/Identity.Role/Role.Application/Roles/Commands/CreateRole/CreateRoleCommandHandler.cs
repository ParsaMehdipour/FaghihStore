using MediatR;
using Microsoft.AspNetCore.Identity;
using Role.Domain.Models;
using SH.Application.Interfaces;

namespace Role.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
    protected ICurrentUserService _currentUserService { get; }
    protected RoleManager<ApplicationRole> _roleManager { get; }
    protected RoleFactory _roleFactory { get; }

    public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager, RoleFactory roleFactory, ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _roleManager = roleManager;
        _roleFactory = roleFactory;
    }

    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        ApplicationRole role = _roleFactory.Create(request.Name, request.DisplayName, _currentUserService.UserId);

        await _roleManager.CreateAsync(role);

        return Result.Ok(role.Id);
    }
}
