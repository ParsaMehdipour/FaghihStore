using MediatR;

using TG.Domain.Models;

namespace TG.Application.TraitGroups.Commands.EditTraitGroup;

public class EditTraitGroupCommandHandler : IRequestHandler<EditTraitGroupCommand, Result<Guid>>
{
    protected ITraitGroupRepository _traitGroupRepository { get; set; }

    public EditTraitGroupCommandHandler(ITraitGroupRepository traitGroupRepository)
    {
        _traitGroupRepository = traitGroupRepository;
    }

    public async Task<Result<Guid>> Handle(EditTraitGroupCommand request, CancellationToken cancellationToken)
    {
        TraitGroup traitGroup = await _traitGroupRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(traitGroup, nameof(traitGroup));

        traitGroup.SetTitle(request.Title);
        traitGroup.SetOrderNumber(request.OrderNumber);
        traitGroup.SetModifiedDate(DateTime.Now);

        await _traitGroupRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
