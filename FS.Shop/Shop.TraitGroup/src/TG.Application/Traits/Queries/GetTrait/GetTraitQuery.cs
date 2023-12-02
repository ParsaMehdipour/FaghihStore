using MediatR;

using TG.Application.Traits.Commands.EditTrait;
using TG.Domain.Models;

namespace TG.Application.Traits.Queries.GetTrait;

public record GetTraitQuery(Guid Id) : IRequest<Result<EditTraitCommand>>
{
    internal EditTraitCommand ToCommand(Trait trait) =>
        new(trait.Id,
            trait.Title, trait.OrderNumber, trait.HasFilterAbility,
            trait.TraitGroupId, trait.CategoryId);
}