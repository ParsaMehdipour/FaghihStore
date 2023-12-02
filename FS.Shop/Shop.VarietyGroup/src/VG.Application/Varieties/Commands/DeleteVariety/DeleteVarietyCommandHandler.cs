using FluentResults;
using MediatR;
using VG.Domain.Models;
using VG.Domain.Repositories;

namespace VG.Application.Varieties.Commands.DeleteVariety;

public class DeleteVarietyCommandHandler : IRequestHandler<DeleteVarietyCommand, Result>
{

    protected IVarietyRepository _varietyRepository { get; }

    public DeleteVarietyCommandHandler(IVarietyRepository varietyRepository)
    {
        _varietyRepository = varietyRepository;
    }

    public async Task<Result> Handle(DeleteVarietyCommand request, CancellationToken cancellationToken)
    {
        Variety variety = await _varietyRepository.GetWithoutQueryFilterAsync(_ => _.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(variety, nameof(variety));

        if (request.IsRestored is true)
            variety.RestoreItem();
        else
            variety.DeleteItem();

        await _varietyRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
