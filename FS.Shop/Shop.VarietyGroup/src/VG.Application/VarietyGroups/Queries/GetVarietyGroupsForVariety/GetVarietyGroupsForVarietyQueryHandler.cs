using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VG.Domain.Repositories;

namespace VG.Application.VarietyGroups.Queries.GetVarietyGroupsForVariety;

public class GetVarietyGroupsForVarietyQueryHandler : IRequestHandler<GetVarietyGroupsForVarietyQuery, Result<IEnumerable<GetVarietyGroupForVarietyDto>>>
{
    protected IVarietyGroupRepository _varietyGroupRepository { get; }

    public GetVarietyGroupsForVarietyQueryHandler(IVarietyGroupRepository varietyGroupRepository)
    {
        _varietyGroupRepository = varietyGroupRepository;
    }

    public async Task<Result<IEnumerable<GetVarietyGroupForVarietyDto>>> Handle(GetVarietyGroupsForVarietyQuery request, CancellationToken cancellationToken)
    {
        var varietyGroups = _varietyGroupRepository.Get();

        var result = await varietyGroups.Select(_ => new GetVarietyGroupForVarietyDto(
            _.Id,
            _.Title)).ToListAsync(cancellationToken);

        IEnumerable<GetVarietyGroupForVarietyDto> responseWithReadOnly = result.AsReadOnly();

        return Result.Ok(responseWithReadOnly);
    }
}
