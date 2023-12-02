using SH.Application.Interfaces;
using SH.Application.Models;

namespace PB.Infrastructure.EfCore;

public class ProductBrandManagementPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<RoleClaimsViewModel>> Expose()
    {
        return new Dictionary<string, List<RoleClaimsViewModel>>
        {
            {
                "ProductBrand", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.PRODUCTBRANDS_FullManagement, Type = "Permission"},
                }
            }
        };
    }

    public static class Permissions
    {
        public const string PRODUCTBRANDS_FullManagement = "Permissions.ProductBrands.FullManagement";
    }
}