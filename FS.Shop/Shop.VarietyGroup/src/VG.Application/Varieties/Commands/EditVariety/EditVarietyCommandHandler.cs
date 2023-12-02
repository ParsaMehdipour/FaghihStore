using FluentResults;
using MediatR;
using VG.Domain.Models;
using VG.Domain.Repositories;

namespace VG.Application.Varieties.Commands.EditVariety;

public class EditVarietyCommandHandler : IRequestHandler<EditVarietyCommand, Result>
{

    protected IVarietyRepository _varietyRepository { get; }

    public EditVarietyCommandHandler(IVarietyRepository varietyRepository)
    {
        _varietyRepository = varietyRepository;
    }

    public async Task<Result> Handle(EditVarietyCommand request, CancellationToken cancellationToken)
    {
        Variety variety = await _varietyRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(variety, nameof(variety));

        variety.SetTitle(request.Title);
        variety.SetBoxType(request.BoxType);
        variety.SetVarietyGroupId(request.VarietyGroupId);
        variety.SetColorCode(request.ColorCode);
        variety.SetSize(request.Size);

        _varietyRepository.Update(variety);
        await _varietyRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
