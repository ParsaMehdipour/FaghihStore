using FluentResults;

using MediatR;

namespace TG.Application.TraitGroups.Commands.CreateTraitGroup;

public record CreateTraitGroupCommand(string Title, int OrderNumber) : IRequest<Result<Guid>>;