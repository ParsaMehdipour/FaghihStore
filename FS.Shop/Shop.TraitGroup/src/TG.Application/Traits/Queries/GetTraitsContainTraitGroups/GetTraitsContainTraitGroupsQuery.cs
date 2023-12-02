using MediatR;

using SH.Infrastructure.Criteria;

namespace TG.Application.Traits.Queries.GetTraitsContainTraitGroups;
public record GetTraitsContainTraitGroupsQuery() : IRequest<Result<ResponseModel<IEnumerable<GetTraitsContainTraitGroupsDto>>>>;

public record GetTraitsContainTraitGroupsDto(Guid Id, string Trait, string TraitGroup);