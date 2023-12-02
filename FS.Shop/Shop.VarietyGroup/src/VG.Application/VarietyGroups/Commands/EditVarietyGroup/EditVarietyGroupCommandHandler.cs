using FluentResults;
using MediatR;
using VG.Domain.Models;
using VG.Domain.Repositories;

namespace VG.Application.VarietyGroups.Commands.EditVarietyGroup;

public class EditVarietyGroupCommandHandler : IRequestHandler<EditVarietyGroupCommand, Result>
{
    private IVarietyGroupRepository _varietyGroupRepository { get; }

    public EditVarietyGroupCommandHandler(IVarietyGroupRepository varietyGroupRepository)
    {
        this._varietyGroupRepository = varietyGroupRepository;
    }

    public async Task<Result> Handle(EditVarietyGroupCommand request, CancellationToken cancellationToken)
    {
        VarietyGroup varietyGroup = await _varietyGroupRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(varietyGroup, nameof(varietyGroup));

        varietyGroup.SetTitle(request.Title);

        _varietyGroupRepository.Update(varietyGroup);
        await _varietyGroupRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
