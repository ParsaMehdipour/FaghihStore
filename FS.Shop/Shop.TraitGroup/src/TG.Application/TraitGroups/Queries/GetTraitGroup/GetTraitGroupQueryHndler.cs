using MediatR;

using TG.Application.TraitGroups.Commands.EditTraitGroup;
using TG.Domain.Models;

namespace TG.Application.TraitGroups.Queries.GetTraitGroup;

public class GetTraitGroupQueryHndler : IRequestHandler<GetTraitGroupQuery, Result<EditTraitGroupCommand>>
{
    public ITraitGroupRepository _traitGroupRepository { get; }

    public GetTraitGroupQueryHndler(ITraitGroupRepository traitGroupRepository)
    {
        _traitGroupRepository = traitGroupRepository;
    }

    public async Task<Result<EditTraitGroupCommand>> Handle(GetTraitGroupQuery request, CancellationToken cancellationToken)
    {
        TraitGroup traitGroup = await _traitGroupRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(traitGroup, nameof(traitGroup));

        return Result.Ok(request.ToCommand(traitGroup));
    }
}