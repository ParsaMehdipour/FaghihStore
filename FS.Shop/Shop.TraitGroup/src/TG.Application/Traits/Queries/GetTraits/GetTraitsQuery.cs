using MediatR;

using SH.Infrastructure.Criteria;

using TG.Application.Criteria;

namespace TG.Application.Traits.Queries.GetTraits;
public record GetTraitsQuery(TraitQueryStringParameter Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetTraitDto>>>>;

public record GetTraitDto(Guid Id, string Title, string TraitGroup, string Category, string CreateDate, bool IsDeleted);