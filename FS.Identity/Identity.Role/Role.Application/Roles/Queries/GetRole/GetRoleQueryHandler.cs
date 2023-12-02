using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Role.Application.Roles.Commands.EditRole;
using Role.Domain.Models;

using SH.Application.Interfaces;

namespace Role.Application.Roles.Queries.GetRole;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Result<EditRoleCommand>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    protected IEnumerable<IPermissionExposer> _exposers { get; }

    public GetRoleQueryHandler(RoleManager<ApplicationRole> roleManager, IEnumerable<IPermissionExposer> exposers)
    {
        _roleManager = roleManager;
        _exposers = exposers;
    }

    public async Task<Result<EditRoleCommand>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        List<SelectListItem> currentPermissions = new();

        ApplicationRole role = await _roleManager.Roles.IgnoreQueryFilters().FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken);

        var roleClaims = await _roleManager.GetClaimsAsync(role);

        foreach (var exposer in _exposers)
        {
            var exposedPermissions = exposer.Expose();

            foreach (var (key, value) in exposedPermissions)
            {
                var group = new SelectListGroup { Name = key };

                foreach (var permission in value)
                {
                    var item = new SelectListItem(permission.Value.Split(".")[2], permission.Value)
                    {
                        Group = group
                    };

                    if (roleClaims.Any(x => x.Value == permission.Value))
                        item.Selected = true;

                    currentPermissions.Add(item);
                }
            }
        }

        return Result.Ok(request.ToCommand(role, null, currentPermissions));
    }
}