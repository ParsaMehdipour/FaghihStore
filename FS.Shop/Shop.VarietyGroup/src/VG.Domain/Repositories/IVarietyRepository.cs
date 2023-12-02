using SH.Domain.Interfaces;
using VG.Domain.Models;

namespace VG.Domain.Repositories;

public interface IVarietyRepository : IBaseRepository<Variety>
{
    Task<bool> TitleExists(Guid id, string title, bool isForModify, CancellationToken cancellationToken);
    Task<bool> ColorCodeExists(Guid id, string colorCode, bool isForModify, CancellationToken cancellationToken);
    Task<bool> SizeExists(Guid id, string size, bool isForModify, CancellationToken cancellationToken);
}
