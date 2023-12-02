using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Role.Domain.Models;

namespace Role.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
{

    protected RoleManager<ApplicationRole> _roleManager { get; }
    protected IRoleRepository _roleRepository { get; }

    public DeleteRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IRoleRepository roleRepository)
    {
        _roleManager = roleManager;
        _roleRepository = roleRepository;
    }

    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        ApplicationRole role = await _roleManager.Roles.IgnoreQueryFilters().SingleOrDefaultAsync(_ => _.Id.Equals(request.Id), cancellationToken);

        ArgumentNullException.ThrowIfNull(role, nameof(role));

        if (request.IsRestored is false)
            role.DeleteItem();
        else
            role.RestoreItem();

        await _roleRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
