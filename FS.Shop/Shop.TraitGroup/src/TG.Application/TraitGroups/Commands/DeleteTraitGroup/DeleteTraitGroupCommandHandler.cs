using MediatR;

using TG.Domain.Models;

namespace VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

public class DeleteTraitGroupCommandHandler : IRequestHandler<DeleteTraitGroupCommand, Result>
{
    protected ITraitGroupRepository _traitGroupRepository { get; set; }

    public DeleteTraitGroupCommandHandler(ITraitGroupRepository traitGroupRepository)
    {
        _traitGroupRepository = traitGroupRepository;
    }

    public async Task<Result> Handle(DeleteTraitGroupCommand request, CancellationToken cancellationToken)
    {
        TraitGroup traitGroup = await _traitGroupRepository.GetWithoutQueryFilterAsync(_ => _.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(traitGroup, nameof(traitGroup));

        if (request.IsRestored is true) traitGroup.RestoreItem();
        else traitGroup.DeleteItem();

        await _traitGroupRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
