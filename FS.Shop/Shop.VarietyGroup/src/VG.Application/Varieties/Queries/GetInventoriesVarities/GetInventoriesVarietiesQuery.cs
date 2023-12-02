using MediatR;

namespace VG.Application.Varieties.Queries.GetInventoriesVarities;

public record GetInventoriesVarietiesQuery(Dictionary<Guid, Guid> VarietyDictionary) : IRequest<Dictionary<Guid, string>>;
