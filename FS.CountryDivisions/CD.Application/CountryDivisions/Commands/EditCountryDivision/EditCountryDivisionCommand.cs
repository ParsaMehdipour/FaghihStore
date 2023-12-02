using FluentResults;

using MediatR;

namespace CD.Application.CountryDivisions.Commands.EditCountryDivision;

public record EditCountryDivisionCommand(Guid Id, string Name, Guid ParentId) : IRequest<Result>;
