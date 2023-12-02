using FluentResults;
using MediatR;
using SH.Infrastructure.Criteria;
using VG.Application.Criteria;

namespace VG.Application.Varieties.Queries.GetVarieties;

public record GetVarietiesQuery(VarietyQueryStringParameter Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetVarietyDto>>>>;

public record GetVarietyDto(Guid Id, string Title, string ColorCode, string Size, string VarietyGroup, string CreateDate, bool IsDeleted);
