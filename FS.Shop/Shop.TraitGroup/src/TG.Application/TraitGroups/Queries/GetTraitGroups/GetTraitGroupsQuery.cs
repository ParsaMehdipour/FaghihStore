using MediatR;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Searching;

namespace TG.Application.TraitGroups.Queries.GetTraitGroups;
public record GetTraitGroupsQuery(QueryStringParameters Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetTraitGroupDto>>>>;

public record GetTraitGroupDto(Guid Id, string Title, string CreateDate, bool IsDeleted);