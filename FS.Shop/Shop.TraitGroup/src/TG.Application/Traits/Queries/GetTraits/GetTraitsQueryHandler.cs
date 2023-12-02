using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;
using SH.Infrastructure.Services;
using SH.Infrastructure.Settings;

namespace TG.Application.Traits.Queries.GetTraits;

public class GetTraitsQueryHandler : IRequestHandler<GetTraitsQuery, Result<ResponseModel<IEnumerable<GetTraitDto>>>>
{
    protected ITraitRepository _traitRepository { get; }
    protected HttpClientService _httpClientService { get; }
    protected List<ProjectsUrls> ProjectsUrls { get; }

    public GetTraitsQueryHandler(ITraitRepository traitRepository,
        HttpClientService httpClientService,
        IOptions<SiteSettings> options)
    {
        _traitRepository = traitRepository;
        _httpClientService = httpClientService;
        ProjectsUrls = options.Value.ProjectsUrls;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetTraitDto>>>> Handle(GetTraitsQuery request, CancellationToken cancellationToken)
    {
        var traits = _traitRepository.Get().IgnoreQueryFilters().Where(_ => _.IsDeleted == request.Parameters.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            traits = traits.Where(_ => _.Title.Contains(request.Parameters.Search));

        int count = await traits.CountAsync(cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        var traitCategories = traits.ToDictionary(_ => _.Id, _ => _.CategoryId);

        _httpClientService.SetBaseAddress(ProjectsUrls.FirstOrDefault(_ => _.Project.Equals("Category.Api")).Url);
        var categories = await _httpClientService.Send<Dictionary<Guid, Guid>, Dictionary<Guid, string>>(traitCategories, "api/category/getOwnModelCategories/", cancellationToken);

        var result = await traits.Paginate(pager).Select(_ => new GetTraitDto(
                _.Id,
                _.Title,
                _.TraitGroup.Title,
                 categories[_.Id],
                _.CreatedDate.ToPersian(),
                _.IsDeleted))
            .ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetTraitDto>> responseModel =
            new()
            {
                Model = result.AsReadOnly(),
                Pager = pager,
                Parameters = request.Parameters
            };

        return Result.Ok(responseModel);
    }
}
