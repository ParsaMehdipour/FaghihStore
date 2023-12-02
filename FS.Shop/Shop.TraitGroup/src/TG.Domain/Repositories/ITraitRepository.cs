using SH.Domain.Interfaces;

using TG.Domain.Models;

namespace TG.Domain.Repositories;

public interface ITraitRepository : IBaseRepository<Trait>
{
    Task<bool> TitleExists(Guid id, string title, bool isForModify, CancellationToken cancellationToken);
    Task<bool> TraitGroupIdExists(Guid traitGroupId, CancellationToken cancellationToken);
    Task<bool> CategoryIdExists(Guid categoryId, CancellationToken cancellationToken);
}