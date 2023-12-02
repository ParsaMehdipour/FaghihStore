using FluentResults;
using MediatR;
using VG.Application.VarietyGroups.Commands.EditVarietyGroup;
using VG.Domain.Models;
using VG.Domain.Repositories;

namespace VG.Application.VarietyGroups.Queries.GetVarietyGroup;

public class GetVarietyGroupQueryHandler : IRequestHandler<GetVarietyGroupQuery, Result<EditVarietyGroupCommand>>
{
    protected IVarietyGroupRepository _varietyGroupRepository { get; }

    public GetVarietyGroupQueryHandler(IVarietyGroupRepository varietyGroupRepository)
    {
        _varietyGroupRepository = varietyGroupRepository;
    }

    public async Task<Result<EditVarietyGroupCommand>> Handle(GetVarietyGroupQuery request, CancellationToken cancellationToken)
    {
        VarietyGroup varietyGroup = await _varietyGroupRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(varietyGroup, nameof(varietyGroup));

        return Result.Ok(request.ToCommand(varietyGroup));
    }
}
