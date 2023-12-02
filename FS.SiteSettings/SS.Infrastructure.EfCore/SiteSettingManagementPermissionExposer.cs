using SH.Application.Interfaces;
using SH.Application.Models;

namespace SS.Infrastructure.EfCore;

public class SiteSettingManagementPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<RoleClaimsViewModel>> Expose()
    {
        return new Dictionary<string, List<RoleClaimsViewModel>>
        {
            {
                "SiteSetting", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.SITESETTING_FullManagement, Type = "Permission"},
                }
            },
            {
                "SitePanelSender", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.SITEPANELSENDER_FullManagement, Type = "Permission"},
                }
            }
        };
    }

    public static class Permissions
    {
        public const string SITESETTING_FullManagement = "Permissions.SiteSettings.FullManagement";

        public const string SITEPANELSENDER_FullManagement = "Permissions.SitePanelSenders.FullManagement";
    }
}