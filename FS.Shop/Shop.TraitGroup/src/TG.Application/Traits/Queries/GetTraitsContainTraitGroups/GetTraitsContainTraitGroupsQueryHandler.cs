using MediatR;

using Microsoft.EntityFrameworkCore;

using SH.Infrastructure.Criteria;

namespace TG.Application.Traits.Queries.GetTraitsContainTraitGroups;

public class GetTraitsContainTraitGroupsQueryHandler : IRequestHandler<GetTraitsContainTraitGroupsQuery, Result<ResponseModel<IEnumerable<GetTraitsContainTraitGroupsDto>>>>
{
    protected ITraitRepository _traitRepository { get; }

    public GetTraitsContainTraitGroupsQueryHandler(ITraitRepository traitRepository)
    {
        _traitRepository = traitRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetTraitsContainTraitGroupsDto>>>> Handle(GetTraitsContainTraitGroupsQuery request, CancellationToken cancellationToken)
    {
        var traits = await _traitRepository.Get()
            .AsNoTracking()
            .Include(_ => _.TraitGroup)
            .OrderBy(_ => _.TraitGroup.Title)
            .Select(_ => new GetTraitsContainTraitGroupsDto(_.Id, _.Title, _.TraitGroup.Title))
            .ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetTraitsContainTraitGroupsDto>> responseModel =
            new()
            {
                Model = traits.AsReadOnly(),
            };

        return Result.Ok(responseModel);
    }
}