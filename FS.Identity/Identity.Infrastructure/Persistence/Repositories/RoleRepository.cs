using Identity.Infrastructure.Helpers;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Role.Domain.Models;
using Role.Domain.Repositories;

using SH.Infrastructure.EfCore.Repositories;

namespace Identity.Infrastructure.Persistence.Repositories;

public class RoleRepository : EfRepository<ApplicationRole>, IRoleRepository
{
    protected IdentityContext _identityContext { get; }
    protected RoleManager<ApplicationRole> _roleManager { get; }

    public RoleRepository(IdentityContext context, RoleManager<ApplicationRole> roleManager) : base(context)
    {
        _identityContext = context;
        _roleManager = roleManager;
    }

    public async Task<bool> NameBeUnique(Guid id, string name, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.Name.Equals(name), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.Name.Equals(name), cancellationToken);

        return isExists;
    }

    public async Task<bool> DisplayNameBeUnique(Guid id, string displayName, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.DisplayName.Equals(displayName), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.DisplayName.Equals(displayName), cancellationToken);

        return isExists;
    }

    public async Task<string> GetRoleNameByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await (from userRole in _identityContext.UserRoles
                      join role in _identityContext.Roles on userRole.RoleId equals role.Id
                      where userRole.UserId.Equals(userId)
                      select role.Name).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<string> GetRoleDisplayNameByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await (from userRole in _identityContext.UserRoles
                      join role in _identityContext.Roles on userRole.RoleId equals role.Id
                      where userRole.UserId.Equals(userId)
                      select role.DisplayName).FirstOrDefaultAsync(cancellationToken);
    }

    public string GetRoleDisplayNameByUser(Guid userId)
    {
        return (from userRole in _identityContext.UserRoles
                join role in _identityContext.Roles on userRole.RoleId equals role.Id
                where userRole.UserId.Equals(userId)
                select role.DisplayName).FirstOrDefault();
    }

    public async Task AddPermissionsToRole(ApplicationRole role, IList<string> permissions, CancellationToken cancellationToken)
    {
        var claims = await _roleManager.GetClaimsAsync(role);

        foreach (var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        var selectedClaims = permissions.ToList();

        foreach (var claim in selectedClaims)
        {
            await _roleManager.AddPermissionClaim(role, claim);
        }
    }
}