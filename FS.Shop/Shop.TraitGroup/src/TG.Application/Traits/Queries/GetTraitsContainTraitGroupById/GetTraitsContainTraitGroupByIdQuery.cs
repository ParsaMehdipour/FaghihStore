using MediatR;

using SH.Infrastructure.Criteria;

namespace TG.Application.Traits.Queries.GetTraitsContainTraitGroupById;

public record GetTraitsContainTraitGroupByIdQuery(Guid[] traitsId) : IRequest<Result<ResponseModel<IEnumerable<GetTraitsContainTraitGroupByIdDto>>>>;

public record GetTraitsContainTraitGroupByIdDto(Guid Id, string Trait, string TraitGroup);