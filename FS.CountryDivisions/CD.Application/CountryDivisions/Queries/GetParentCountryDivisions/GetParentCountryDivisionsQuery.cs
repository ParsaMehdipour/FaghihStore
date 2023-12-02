using FluentResults;
using MediatR;

namespace CD.Application.CountryDivisions.Queries.GetParentCountryDivisions;

public record GetParentCountryDivisionsQuery() : IRequest<Result<IEnumerable<GetParentCountryDivisionDto>>>;

public record GetParentCountryDivisionDto(Guid Id, string Name);
