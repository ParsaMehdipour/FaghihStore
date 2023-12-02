using FluentResults;

using MediatR;

namespace CD.Application.CountryDivisions.Commands.CreateCountryDivision;

public record CreateCountryDivisionCommand(string Name, Guid ParentId) : IRequest<Result<Guid>>;
