using MediatR;

using Microsoft.AspNetCore.Mvc.Rendering;

using Role.Application.Roles.Commands.EditRole;
using Role.Domain.Models;

namespace Role.Application.Roles.Queries.GetRole;

public record GetRoleQuery(Guid Id) : IRequest<Result<EditRoleCommand>>
{
    internal EditRoleCommand ToCommand(ApplicationRole role, List<string> Permissions, List<SelectListItem> CurrentPermissions) =>
        new(
            role.Id,
            role.Name,
            role.DisplayName,
            Permissions,
            CurrentPermissions
        );
}

