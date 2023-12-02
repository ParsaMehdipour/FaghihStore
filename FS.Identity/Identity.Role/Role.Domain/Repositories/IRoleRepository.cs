using Role.Domain.Models;

using SH.Domain.Interfaces;

namespace Role.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<ApplicationRole>
{
    public Task<bool> NameBeUnique(Guid id, string name, bool isForModify, CancellationToken cancellationToken);
    public Task<bool> DisplayNameBeUnique(Guid id, string displayName, bool isForModify, CancellationToken cancellationToken);
    public Task<string> GetRoleDisplayNameByUserAsync(Guid userId, CancellationToken cancellationToken);
    public Task<string> GetRoleNameByUserAsync(Guid userId, CancellationToken cancellationToken);
    public string GetRoleDisplayNameByUser(Guid userId);
    public Task AddPermissionsToRole(ApplicationRole role, IList<string> permissions, CancellationToken cancellationToken);
}