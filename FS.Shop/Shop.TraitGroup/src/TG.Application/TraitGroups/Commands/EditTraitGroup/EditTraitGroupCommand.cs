using MediatR;

namespace TG.Application.TraitGroups.Commands.EditTraitGroup;

public record EditTraitGroupCommand(Guid Id, string Title, int OrderNumber) : IRequest<Result<Guid>>;