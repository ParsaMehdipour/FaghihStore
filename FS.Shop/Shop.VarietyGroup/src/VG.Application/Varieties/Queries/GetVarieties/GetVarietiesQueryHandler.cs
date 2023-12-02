using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;
using VG.Domain.Repositories;

namespace VG.Application.Varieties.Queries.GetVarieties;

public class GetVarietiesQueryHandler : IRequestHandler<GetVarietiesQuery, Result<ResponseModel<IEnumerable<GetVarietyDto>>>>
{

    protected IVarietyRepository _varietyRepository { get; }

    public GetVarietiesQueryHandler(IVarietyRepository varietyRepository)
    {
        _varietyRepository = varietyRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetVarietyDto>>>> Handle(GetVarietiesQuery request, CancellationToken cancellationToken)
    {
        var varieties = _varietyRepository.Get().IgnoreQueryFilters().Where(_ => _.IsDeleted == request.Parameters.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            varieties = varieties.Where(_ => _.Title.Contains(request.Parameters.Search));

        int count = await varieties.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        var result = await varieties.Include(_ => _.VarietyGroup).Select(_ => new GetVarietyDto(
            _.Id,
            _.Title,
            _.ColorCode,
            _.Size,
            _.VarietyGroup.Title,
            _.CreatedDate.ToPersian(),
            _.IsDeleted)).ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetVarietyDto>> responseModel = new ResponseModel<IEnumerable<GetVarietyDto>>()
        {
            Model = result.AsReadOnly(),
            Pager = pager,
            Parameters = request.Parameters
        };

        return Result.Ok(responseModel);
    }
}
