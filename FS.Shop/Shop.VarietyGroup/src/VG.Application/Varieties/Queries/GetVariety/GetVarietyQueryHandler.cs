using FluentResults;
using MediatR;
using VG.Application.Varieties.Commands.EditVariety;
using VG.Domain.Models;
using VG.Domain.Repositories;

namespace VG.Application.Varieties.Queries.GetVariety;

public class GetVarietyQueryHandler : IRequestHandler<GetVarietyQuery, Result<EditVarietyCommand>>
{

    protected IVarietyRepository _varietyRepository { get; }

    public GetVarietyQueryHandler(IVarietyRepository varietyRepository)
    {
        _varietyRepository = varietyRepository;
    }

    public async Task<Result<EditVarietyCommand>> Handle(GetVarietyQuery request, CancellationToken cancellationToken)
    {
        Variety variety = await _varietyRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(variety);

        return Result.Ok(request.ToCommand(variety));
    }
}
