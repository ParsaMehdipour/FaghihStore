using CD.Application.Criteria;
using FluentResults;
using MediatR;
using SH.Infrastructure.Criteria;

namespace CD.Application.CountryDivisions.Queries.GetCountryDivisions;

public record GetCountryDivisionsQuery(CountryDivisionQueryString Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetCountryDivisionDto>>>>;

public record GetCountryDivisionDto(Guid Id, string Name, string Parent, string CreateDate);