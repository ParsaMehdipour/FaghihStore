using CD.Domain.Repositories;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;

namespace CD.Application.CountryDivisions.Queries.GetCountryDivisions;

public class GetCountryDivisionsQueryHandler : IRequestHandler<GetCountryDivisionsQuery, Result<ResponseModel<IEnumerable<GetCountryDivisionDto>>>>
{
    protected ICountryDivisionRepository _countryDivisionRepository { get; }

    public GetCountryDivisionsQueryHandler(ICountryDivisionRepository countryDivisionRepository)
    {
        _countryDivisionRepository = countryDivisionRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetCountryDivisionDto>>>> Handle(GetCountryDivisionsQuery request, CancellationToken cancellationToken)
    {
        var countryDivisions = _countryDivisionRepository.Get().IgnoreQueryFilters().Where(_ => _.IsDeleted == request.Parameters.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Parameters.Title))
            countryDivisions = countryDivisions.Where(_ => _.Name.Contains(request.Parameters.Title));

        int count = await countryDivisions.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        var result = await countryDivisions.Include(_ => _.Parent).Paginate(pager).Select(_ =>
            new GetCountryDivisionDto(
                _.Id,
                _.Name,
                _.Parent.Name,
                _.CreatedDate.ToPersian())).ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetCountryDivisionDto>> responseModel = new()
        {
            Model = result.AsReadOnly(),
            Pager = pager,
            Parameters = request.Parameters
        };

        return Result.Ok(responseModel);
    }
}
