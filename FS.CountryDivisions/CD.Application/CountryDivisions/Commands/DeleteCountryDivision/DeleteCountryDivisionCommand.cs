using FluentResults;

using MediatR;

namespace CD.Application.CountryDivisions.Commands.DeleteCountryDivision;

public record DeleteCountryDivisionCommand(Guid Id, bool IsRestored) : IRequest<Result>;

