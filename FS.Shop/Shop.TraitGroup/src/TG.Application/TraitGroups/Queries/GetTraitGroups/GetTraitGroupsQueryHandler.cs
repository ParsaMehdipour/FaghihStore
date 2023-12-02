using MediatR;

using Microsoft.EntityFrameworkCore;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;

namespace TG.Application.TraitGroups.Queries.GetTraitGroups;

public class GetTraitGroupsQueryHandler : IRequestHandler<GetTraitGroupsQuery, Result<ResponseModel<IEnumerable<GetTraitGroupDto>>>>
{
    public ITraitGroupRepository _traitGroupRepository { get; }

    public GetTraitGroupsQueryHandler(ITraitGroupRepository traitGroupRepository)
    {
        _traitGroupRepository = traitGroupRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetTraitGroupDto>>>> Handle(GetTraitGroupsQuery request, CancellationToken cancellationToken)
    {
        var traitGroups = _traitGroupRepository.Get().IgnoreQueryFilters().Where(_ => _.IsDeleted == request.Parameters.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            traitGroups = traitGroups.Where(_ => _.Title.Contains(request.Parameters.Search));

        int count = await traitGroups.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        var result = await traitGroups.Paginate(pager).Select(_ => new GetTraitGroupDto(
                _.Id,
                _.Title,
                _.CreatedDate.ToPersian(),
                _.IsDeleted))
            .ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetTraitGroupDto>> responseModel =
            new ResponseModel<IEnumerable<GetTraitGroupDto>>()
            {
                Model = result.AsReadOnly(),
                Pager = pager,
                Parameters = request.Parameters
            };

        return Result.Ok(responseModel);
    }
}
