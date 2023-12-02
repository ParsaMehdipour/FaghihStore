using CD.Application.CountryDivisions.Commands.EditCountryDivision;
using CD.Domain.Models;
using FluentResults;
using MediatR;

namespace CD.Application.CountryDivisions.Queries.GetCountryDivision;

public record GetCountryDivisionQuery(Guid Id) : IRequest<Result<EditCountryDivisionCommand>>
{
    internal EditCountryDivisionCommand toCommand(CountryDivision countryDivision) =>
        new EditCountryDivisionCommand(
            countryDivision.Id,
            countryDivision.Name,
            countryDivision.ParentId.GetValueOrDefault(Guid.Empty));
}
