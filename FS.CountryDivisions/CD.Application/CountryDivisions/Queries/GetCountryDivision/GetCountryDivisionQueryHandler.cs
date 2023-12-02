using CD.Application.CountryDivisions.Commands.EditCountryDivision;
using CD.Domain.Models;
using CD.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CD.Application.CountryDivisions.Queries.GetCountryDivision;

public class GetCountryDivisionQueryHandler : IRequestHandler<GetCountryDivisionQuery, Result<EditCountryDivisionCommand>>
{

    protected ICountryDivisionRepository _countryDivisionRepository { get; }

    public GetCountryDivisionQueryHandler(ICountryDivisionRepository countryDivisionRepository)
    {
        _countryDivisionRepository = countryDivisionRepository;
    }

    public async Task<Result<EditCountryDivisionCommand>> Handle(GetCountryDivisionQuery request, CancellationToken cancellationToken)
    {
        CountryDivision countryDivision = await _countryDivisionRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(countryDivision, nameof(countryDivision));

        return Result.Ok(request.toCommand(countryDivision));
    }
}
