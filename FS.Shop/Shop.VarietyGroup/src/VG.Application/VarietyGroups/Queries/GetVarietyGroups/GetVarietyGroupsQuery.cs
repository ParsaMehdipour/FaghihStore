using FluentResults;
using MediatR;
using SH.Infrastructure.Criteria;
using VG.Application.Criteria;

namespace VG.Application.VarietyGroups.Queries.GetVarietyGroups;

public record GetVarietyGroupsQuery(VarietyGroupQueryStringParameter Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetVarietyGroupDto>>>>;

public record GetVarietyGroupDto(Guid Id, string Title, string CreateDate, bool IsDeleted);

