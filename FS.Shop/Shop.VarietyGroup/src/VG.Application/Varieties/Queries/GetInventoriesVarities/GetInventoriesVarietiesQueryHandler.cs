using MediatR;
using Microsoft.EntityFrameworkCore;
using VG.Domain.Repositories;

namespace VG.Application.Varieties.Queries.GetInventoriesVarities;

public class GetInventoriesVarietiesQueryHandler : IRequestHandler<GetInventoriesVarietiesQuery, Dictionary<Guid, string>>
{
    protected IVarietyRepository _varietyRepository { get; }

    public GetInventoriesVarietiesQueryHandler(IVarietyRepository varietyRepository)
    {
        _varietyRepository = varietyRepository;
    }

    public async Task<Dictionary<Guid, string>> Handle(GetInventoriesVarietiesQuery request, CancellationToken cancellationToken)
    {
        Dictionary<Guid, string> VarietiesDictionary = new();

        var varieties = await _varietyRepository.Get(_ => request.VarietyDictionary.Values.Contains(_.Id)).ToListAsync(cancellationToken);

        foreach (var dictionary in request.VarietyDictionary)
        {
            string title = varieties.FirstOrDefault(_ => _.Id == dictionary.Value)!.Title;

            VarietiesDictionary.Add(dictionary.Key, title);
        }

        return VarietiesDictionary;
    }
}
