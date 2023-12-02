using MediatR;

namespace TG.Application.Traits.Commands.DeleteTrait;

public record DeleteTraitCommand(Guid Id, bool IsRestored) : IRequest<Result>;