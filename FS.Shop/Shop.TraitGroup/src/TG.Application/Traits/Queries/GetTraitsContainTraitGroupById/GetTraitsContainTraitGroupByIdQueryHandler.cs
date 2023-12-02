using MediatR;

using Microsoft.EntityFrameworkCore;

using SH.Infrastructure.Criteria;

namespace TG.Application.Traits.Queries.GetTraitsContainTraitGroupById;

public class GetTraitsContainTraitGroupByIdQueryHandler : IRequestHandler<GetTraitsContainTraitGroupByIdQuery, Result<ResponseModel<IEnumerable<GetTraitsContainTraitGroupByIdDto>>>>
{
    protected ITraitRepository _traitRepository { get; }

    public GetTraitsContainTraitGroupByIdQueryHandler(ITraitRepository traitRepository)
    {
        _traitRepository = traitRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetTraitsContainTraitGroupByIdDto>>>> Handle(GetTraitsContainTraitGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var traits = await _traitRepository.Get()
            .AsNoTracking()
            .Include(_ => _.TraitGroup)
            .Where(trait => request.traitsId.Contains(trait.Id))
            .Select(_ => new GetTraitsContainTraitGroupByIdDto(_.Id, _.Title, _.TraitGroup.Title))
            .ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetTraitsContainTraitGroupByIdDto>> responseModel =
            new()
            {
                Model = traits.AsReadOnly(),
            };

        return Result.Ok(responseModel);
    }
}