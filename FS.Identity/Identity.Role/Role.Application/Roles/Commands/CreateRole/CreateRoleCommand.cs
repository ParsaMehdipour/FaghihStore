using MediatR;

namespace Role.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand(string Name, string DisplayName) : IRequest<Result<Guid>>;