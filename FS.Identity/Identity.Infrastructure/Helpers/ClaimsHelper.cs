using Microsoft.AspNetCore.Identity;

using Role.Domain.Models;


using System.Security.Claims;

namespace Identity.Infrastructure.Helpers;

public static class ClaimsHelper
{
    //using SH.Application.Models;
    //using System.Reflection;
    //public static void GetPermissions(this List<RoleClaimsViewModel> allPermissions, Type policy, string roleId)
    //{
    //    FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
    //    foreach (FieldInfo fi in fields)
    //    {
    //        allPermissions.Add(new RoleClaimsViewModel { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
    //    }
    //}

    public static async Task AddPermissionClaim(this RoleManager<ApplicationRole> roleManager, ApplicationRole role, string permission)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
        {
            await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        }
    }
}