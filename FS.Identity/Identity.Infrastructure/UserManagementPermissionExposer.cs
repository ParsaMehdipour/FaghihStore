using SH.Application.Interfaces;
using SH.Application.Models;

namespace Identity.Infrastructure;

public class UserManagementPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<RoleClaimsViewModel>> Expose()
    {
        return new Dictionary<string, List<RoleClaimsViewModel>>
        {
            {
                "User", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.USER_FullManagement, Type = "Permission"}
                }
            },
            {
                "Role", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.ROLE_FullManagement, Type = "Permission"}
                }
            }
        };
    }

    public static class Permissions
    {
        public const string USER_FullManagement = "Permissions.Users.FullManagement";

        public const string ROLE_FullManagement = "Permissions.Roles.FullManagement";
    }
}