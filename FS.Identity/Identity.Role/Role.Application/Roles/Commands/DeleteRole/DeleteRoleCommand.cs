using MediatR;

namespace Role.Application.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(Guid Id, bool IsRestored) : IRequest<Result>;
