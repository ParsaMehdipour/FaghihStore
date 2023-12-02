using MediatR;

namespace TG.Application.Traits.Commands.EditTrait;

public record EditTraitCommand(Guid Id, string Title, int OrderNumber, bool HasFilterAbility, Guid TraitGroupId, Guid CategoryId) : IRequest<Result<Guid>>;