using SH.Domain.Interfaces;

namespace VG.Domain.Repositories;

public interface IVarietyGroupRepository : IBaseRepository<Models.VarietyGroup>
{
    Task<bool> TitleExists(Guid id, string title, bool isForModify, CancellationToken cancellationToken);
}
