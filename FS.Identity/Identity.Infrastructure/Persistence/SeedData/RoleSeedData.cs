using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Role.Application.Roles;
using Role.Domain.Models;

using SH.Infrastructure.Extensions;

using System.Security.Claims;

namespace Identity.Infrastructure.Persistence.SeedData;

public static class RoleSeedData
{
    public static async Task HandleRoleData(this IApplicationBuilder builder)
    {
        var scope = builder.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var identityContext = services.GetService<IdentityContext>();
        await identityContext.Database.MigrateAsync();
        await identityContext.Database.EnsureCreatedAsync();

        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

        if (await roleManager.Roles.AnyAsync(CancellationToken.None) is false)
        {
            var factory = services.GetRequiredService<RoleFactory>();

            List<ApplicationRole> roles = new()
            {
                factory.Create(IdentityConsts.ROLE_ADMIN, "مدیریت", Guid.Empty),
                factory.Create(IdentityConsts.ROLE_USER, "کاربر", Guid.Empty)
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            services.SeedLogger("Role Data Seeded!");
        }

        await Task.CompletedTask;
    }

    public static async Task AddPermissionClaim(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, string module)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        var allPermissions = Permissions.GeneratePermissionsForModule(module);
        foreach (var permission in allPermissions)
        {
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}