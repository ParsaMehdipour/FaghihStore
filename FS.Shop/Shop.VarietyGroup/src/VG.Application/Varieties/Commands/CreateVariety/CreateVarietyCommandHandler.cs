using FluentResults;
using MediatR;
using VG.Domain.Models;
using VG.Domain.Repositories;

namespace VG.Application.Varieties.Commands.CreateVariety;

public class CreateVarietyCommandHandler : IRequestHandler<CreateVarietyCommand, Result>
{

    protected IVarietyRepository _varietyRepository { get; }
    protected VarietyFactory _varietyFactory { get; }

    public CreateVarietyCommandHandler(IVarietyRepository varietyRepository, VarietyFactory varietyFactory)
    {
        _varietyRepository = varietyRepository;
        _varietyFactory = varietyFactory;
    }

    public async Task<Result> Handle(CreateVarietyCommand request, CancellationToken cancellationToken)
    {
        Variety variety = _varietyFactory.Create(request.Title, request.ColorCode, request.Size, request.BoxType, request.VarietyGroupId);

        await _varietyRepository.AddAsync(variety, cancellationToken);
        await _varietyRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
