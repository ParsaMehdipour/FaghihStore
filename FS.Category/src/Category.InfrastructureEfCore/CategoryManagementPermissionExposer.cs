using SH.Application.Interfaces;
using SH.Application.Models;

namespace Category.InfrastructureEfCore;
public class CategoryManagementPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<RoleClaimsViewModel>> Expose()
    {
        return new Dictionary<string, List<RoleClaimsViewModel>>
        {
            {
                "Category", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.CATEGORY_View, Type = "Permission"},
                    new(){Value = Permissions.CATEGORY_Create, Type = "Permission"},
                    new(){Value = Permissions.CATEGORY_Edit, Type = "Permission"},
                    new(){Value = Permissions.CATEGORY_Delete, Type = "Permission"}
                }
            }
        };
    }

    public static class Permissions
    {
        public const string CATEGORY_View = "Permissions.Categories.View";
        public const string CATEGORY_Create = "Permissions.Categories.Create";
        public const string CATEGORY_Edit = "Permissions.Categories.Edit";
        public const string CATEGORY_Delete = "Permissions.Categories.Delete";
    }
}