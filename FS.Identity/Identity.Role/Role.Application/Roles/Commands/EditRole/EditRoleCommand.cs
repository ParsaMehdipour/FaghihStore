using MediatR;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Role.Application.Roles.Commands.EditRole;

public record EditRoleCommand(Guid Id, string Name, string DisplayName, List<string> Permissions, List<SelectListItem> CurrentPermissions) : IRequest<Result>;