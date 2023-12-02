using SH.Domain.Interfaces;

using TG.Domain.Models;

namespace TG.Domain.Repositories;

public interface ITraitGroupRepository : IBaseRepository<TraitGroup>
{
    Task<bool> TitleExists(Guid id, string title, bool isForModify, CancellationToken cancellationToken);
}
