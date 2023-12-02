using SH.Application.Interfaces;
using SH.Application.Models;

namespace VG.Infrastructure.EfCore;
public class VarietyGroupManagementPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<RoleClaimsViewModel>> Expose()
    {
        return new Dictionary<string, List<RoleClaimsViewModel>>
        {
            {
                "VarietyGroup", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.VARIETYGROUP_View, Type = "Permission"},
                    new(){Value = Permissions.VARIETYGROUP_Create, Type = "Permission"},
                    new(){Value = Permissions.VARIETYGROUP_Edit, Type = "Permission"},
                    new(){Value = Permissions.VARIETYGROUP_Delete, Type = "Permission"}
                }
            },
            {
                "Variety", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.VARIETY_View, Type = "Permission"},
                    new(){Value = Permissions.VARIETY_Create, Type = "Permission"},
                    new(){Value = Permissions.VARIETY_Edit, Type = "Permission"},
                    new(){Value = Permissions.VARIETY_Delete, Type = "Permission"}
                }
            }
        };
    }

    public static class Permissions
    {
        public const string VARIETYGROUP_View = "Permissions.VarietyGroups.View";
        public const string VARIETYGROUP_Create = "Permissions.VarietyGroups.Create";
        public const string VARIETYGROUP_Edit = "Permissions.VarietyGroups.Edit";
        public const string VARIETYGROUP_Delete = "Permissions.VarietyGroups.Delete";

        public const string VARIETY_View = "Permissions.Varieties.View";
        public const string VARIETY_Create = "Permissions.Varieties.Create";
        public const string VARIETY_Edit = "Permissions.Varieties.Edit";
        public const string VARIETY_Delete = "Permissions.Varieties.Delete";
    }
}