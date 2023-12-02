using MediatR;

using Microsoft.AspNetCore.Identity;

using Role.Domain.Models;

using SH.Application.Interfaces;

namespace Role.Application.Roles.Commands.EditRole;

public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, Result>
{
    protected RoleManager<ApplicationRole> _roleManager { get; }
    protected IRoleRepository _roleRepository { get; }
    protected ICurrentUserService _currentUserService { get; }

    public EditRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IRoleRepository roleRepository, ICurrentUserService currentUserService)
    {
        _roleManager = roleManager;
        _roleRepository = roleRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        ApplicationRole role = await _roleManager.FindByIdAsync(request.Id.ToString());

        ArgumentNullException.ThrowIfNull(role, nameof(role));

        await _roleRepository.AddPermissionsToRole(role, request.Permissions, cancellationToken);

        role.SetDisplayName(request.DisplayName);
        role.SetName(request.Name);
        role.SetModifiedDate(DateTime.Now, _currentUserService.UserId);

        await _roleRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}