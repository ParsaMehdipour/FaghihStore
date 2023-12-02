using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;
using VG.Domain.Repositories;

namespace VG.Application.VarietyGroups.Queries.GetVarietyGroups;

public class GetVarietyGroupsQueryHandler : IRequestHandler<GetVarietyGroupsQuery, Result<ResponseModel<IEnumerable<GetVarietyGroupDto>>>>
{
    private IVarietyGroupRepository _varietyGroupRepository { get; }

    public GetVarietyGroupsQueryHandler(IVarietyGroupRepository varietyGroupRepository)
    {
        _varietyGroupRepository = varietyGroupRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetVarietyGroupDto>>>> Handle(GetVarietyGroupsQuery request, CancellationToken cancellationToken)
    {
        var varietyGroups = _varietyGroupRepository.Get().IgnoreQueryFilters().Where(_ => _.IsDeleted == request.Parameters.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            varietyGroups = varietyGroups.Where(_ => _.Title.Contains(request.Parameters.Search));

        int count = await varietyGroups.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        var result = await varietyGroups.Paginate(pager).Select(_ => new GetVarietyGroupDto(
                _.Id,
                _.Title,
                _.CreatedDate.ToPersian(),
                _.IsDeleted))
            .ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetVarietyGroupDto>> responseModel =
            new ResponseModel<IEnumerable<GetVarietyGroupDto>>()
            {
                Model = result.AsReadOnly(),
                Pager = pager,
                Parameters = request.Parameters
            };

        return Result.Ok(responseModel);
    }
}
