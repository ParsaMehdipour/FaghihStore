using MediatR;

using PM.Domain.Services;

using TG.Application.Traits.Queries.GetTraitsContainTraitGroupById;

namespace PM.Infrastructure.ModulesAcl;

public class ProductTraitGroupAcl : IProductTraitGroupAcl
{
    protected IMediator _mediator { get; }

    public ProductTraitGroupAcl(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<(Guid traitId, string trait, string traitGroup)>> GetTraitsById(Guid[] traitsId, CancellationToken cancellationToken)
    {
        var traits = await _mediator.Send(new GetTraitsContainTraitGroupByIdQuery(traitsId), cancellationToken);

        List<(Guid traitId, string trait, string traitGroup)> result = new(traits.Value.Model.Count());

        foreach (var trait in traits.Value.Model)
            result.Add((trait.Id, trait.Trait, trait.TraitGroup));

        return result;
    }
}