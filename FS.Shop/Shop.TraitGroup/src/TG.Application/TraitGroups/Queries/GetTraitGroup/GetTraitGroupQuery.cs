using MediatR;

using TG.Application.TraitGroups.Commands.EditTraitGroup;
using TG.Domain.Models;

namespace TG.Application.TraitGroups.Queries.GetTraitGroup;

public record GetTraitGroupQuery(Guid Id) : IRequest<Result<EditTraitGroupCommand>>
{
    internal EditTraitGroupCommand ToCommand(TraitGroup traitGroup) =>
        new(traitGroup.Id,
            traitGroup.Title, traitGroup.OrderNumber);
}