using CD.Domain.Repositories;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CD.Application.CountryDivisions.Queries.GetParentCountryDivisions;

public class GetParentCountryDivisionsQueryHandler : IRequestHandler<GetParentCountryDivisionsQuery, Result<IEnumerable<GetParentCountryDivisionDto>>>
{

    protected ICountryDivisionRepository _countryDivisionRepository { get; }

    public GetParentCountryDivisionsQueryHandler(ICountryDivisionRepository countryDivisionRepository)
    {
        _countryDivisionRepository = countryDivisionRepository;
    }

    public async Task<Result<IEnumerable<GetParentCountryDivisionDto>>> Handle(GetParentCountryDivisionsQuery request, CancellationToken cancellationToken)
    {
        var countryDivisions = _countryDivisionRepository.Get(_ => _.ParentId == null);

        var result = await countryDivisions.Select(_ => new GetParentCountryDivisionDto(
                _.Id,
                _.Name))
            .ToListAsync(cancellationToken);

        IEnumerable<GetParentCountryDivisionDto> responseWithReadOnly = result.AsReadOnly();

        return Result.Ok(responseWithReadOnly);
    }
}
