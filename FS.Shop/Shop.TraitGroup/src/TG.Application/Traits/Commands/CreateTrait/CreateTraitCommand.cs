using MediatR;

namespace TG.Application.Traits.Commands.CreateTrait;

public record CreateTraitCommand(string Title, int OrderNumber, bool HasFilterAbility, Guid TraitGroupId, Guid CategoryId) : IRequest<Result<Guid>>;