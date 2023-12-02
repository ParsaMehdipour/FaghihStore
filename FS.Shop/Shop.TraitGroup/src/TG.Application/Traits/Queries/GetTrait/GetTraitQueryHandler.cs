using MediatR;

using TG.Application.Traits.Commands.EditTrait;
using TG.Domain.Models;

namespace TG.Application.Traits.Queries.GetTrait;

public class GetTraitQueryHandler : IRequestHandler<GetTraitQuery, Result<EditTraitCommand>>
{
    protected ITraitRepository _traitRepository { get; }

    public GetTraitQueryHandler(ITraitRepository traitRepository)
    {
        _traitRepository = traitRepository;
    }

    public async Task<Result<EditTraitCommand>> Handle(GetTraitQuery request, CancellationToken cancellationToken)
    {
        Trait trait = await _traitRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(trait, nameof(trait));

        return Result.Ok(request.ToCommand(trait));
    }
}