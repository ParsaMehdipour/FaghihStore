using MediatR;

using TG.Domain.Models;

namespace TG.Application.Traits.Commands.DeleteTrait;

public class DeleteTraitCommandHandler : IRequestHandler<DeleteTraitCommand, Result>
{
    protected ITraitRepository _traitRepository { get; }

    public DeleteTraitCommandHandler(ITraitRepository traitRepository)
    {
        _traitRepository = traitRepository;
    }

    public async Task<Result> Handle(DeleteTraitCommand request, CancellationToken cancellationToken)
    {
        Trait trait = await _traitRepository.GetWithoutQueryFilterAsync(_ => _.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(trait, nameof(trait));

        if (request.IsRestored is true)
            trait.RestoreItem();
        else
            trait.DeleteItem();

        await _traitRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}