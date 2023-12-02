using SH.Application.Interfaces;
using SH.Application.Models;

namespace TG.Infrastructure.EfCore;

public class TraitGroupManagementPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<RoleClaimsViewModel>> Expose()
    {
        return new Dictionary<string, List<RoleClaimsViewModel>>
        {
            {
                "TraitGroup", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.TRAITGROUP_View, Type = "Permission"},
                    new(){Value = Permissions.TRAITGROUP_Create, Type = "Permission"},
                    new(){Value = Permissions.TRAITGROUP_Edit, Type = "Permission"},
                    new(){Value = Permissions.TRAITGROUP_Delete, Type = "Permission"}
                }
            },
            {
                "Trait", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.TRAIT_View, Type = "Permission"},
                    new(){Value = Permissions.TRAIT_Create, Type = "Permission"},
                    new(){Value = Permissions.TRAIT_Edit, Type = "Permission"},
                    new(){Value = Permissions.TRAIT_Delete, Type = "Permission"}
                }
            }
        };
    }

    public static class Permissions
    {
        public const string TRAITGROUP_View = "Permissions.TraitGroups.View";
        public const string TRAITGROUP_Create = "Permissions.TraitGroups.Create";
        public const string TRAITGROUP_Edit = "Permissions.TraitGroups.Edit";
        public const string TRAITGROUP_Delete = "Permissions.TraitGroups.Delete";

        public const string TRAIT_View = "Permissions.Traits.View";
        public const string TRAIT_Create = "Permissions.Traits.Create";
        public const string TRAIT_Edit = "Permissions.Traits.Edit";
        public const string TRAIT_Delete = "Permissions.Traits.Delete";
    }
}