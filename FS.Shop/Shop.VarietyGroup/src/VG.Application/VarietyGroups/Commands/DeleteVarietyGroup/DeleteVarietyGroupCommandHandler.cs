using FluentResults;
using MediatR;
using VG.Domain.Models;
using VG.Domain.Repositories;

namespace VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

public class DeleteVarietyGroupCommandHandler : IRequestHandler<DeleteVarietyGroupCommand, Result>
{
    private IVarietyGroupRepository _varietyGroupRepository;

    public DeleteVarietyGroupCommandHandler(IVarietyGroupRepository varietyGroupRepository)
    {
        _varietyGroupRepository = varietyGroupRepository;
    }

    public async Task<Result> Handle(DeleteVarietyGroupCommand request, CancellationToken cancellationToken)
    {
        VarietyGroup varietyGroup = await _varietyGroupRepository.GetWithoutQueryFilterAsync(_ => _.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(varietyGroup, nameof(varietyGroup));

        if (request.IsRestored is true) varietyGroup.RestoreItem();
        else varietyGroup.DeleteItem();

        _varietyGroupRepository.Update(varietyGroup);
        await _varietyGroupRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
