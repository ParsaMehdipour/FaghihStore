using SH.Application.Interfaces;
using SH.Application.Models;

using System.Collections.Generic;

namespace PM.Infrastructure.EFCore;
public class ShopManagementPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<RoleClaimsViewModel>> Expose()
    {
        return new Dictionary<string, List<RoleClaimsViewModel>>
        {
            {
                "Product", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.PRODUCT_View, Type = "Permission"},
                    new(){Value = Permissions.PRODUCT_Create, Type = "Permission"},
                    new(){Value = Permissions.PRODUCT_Edit, Type = "Permission"},
                    new(){Value = Permissions.PRODUCT_Delete, Type = "Permission"},
                }
            },
            {
                "ProductDescription", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.PRODUCTDESCRIPTION_View, Type = "Permission"},
                    new(){Value = Permissions.PRODUCTDESCRIPTION_Create, Type = "Permission"},
                    new(){Value = Permissions.PRODUCTDESCRIPTION_Edit, Type = "Permission"},
                    new(){Value = Permissions.PRODUCTDESCRIPTION_Delete, Type = "Permission"},
                }
            },
            {
                "ProductImage", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.PRODUCTIMAGE_View, Type = "Permission"},
                    new(){Value = Permissions.PRODUCTIMAGE_Create, Type = "Permission"},
                    new(){Value = Permissions.PRODUCTIMAGE_Edit, Type = "Permission"},
                    new(){Value = Permissions.PRODUCTIMAGE_Delete, Type = "Permission"},
                }
            },
            {
                "ProductTrait", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.PRODUCTTRAIT_View, Type = "Permission"},
                    new(){Value = Permissions.PRODUCTTRAIT_Create, Type = "Permission"},
                    new(){Value = Permissions.PRODUCTTRAIT_Edit, Type = "Permission"},
                    new(){Value = Permissions.PRODUCTTRAIT_Delete, Type = "Permission"},
                }
            }
        };
    }

    public static class Permissions
    {
        public const string PRODUCT_View = "Permissions.Products.View";
        public const string PRODUCT_Create = "Permissions.Products.Create";
        public const string PRODUCT_Edit = "Permissions.Products.Edit";
        public const string PRODUCT_Delete = "Permissions.Products.Delete";

        public const string PRODUCTDESCRIPTION_View = "Permissions.ProductDescriptions.View";
        public const string PRODUCTDESCRIPTION_Create = "Permissions.ProductDescriptions.Create";
        public const string PRODUCTDESCRIPTION_Edit = "Permissions.ProductDescriptions.Edit";
        public const string PRODUCTDESCRIPTION_Delete = "Permissions.ProductDescriptions.Delete";

        public const string PRODUCTIMAGE_View = "Permissions.Products.View";
        public const string PRODUCTIMAGE_Create = "Permissions.Products.Create";
        public const string PRODUCTIMAGE_Edit = "Permissions.Products.Edit";
        public const string PRODUCTIMAGE_Delete = "Permissions.Products.Delete";

        public const string PRODUCTTRAIT_View = "Permissions.ProductTraits.View";
        public const string PRODUCTTRAIT_Create = "Permissions.ProductTraits.Create";
        public const string PRODUCTTRAIT_Edit = "Permissions.ProductTraits.Edit";
        public const string PRODUCTTRAIT_Delete = "Permissions.ProductTraits.Delete";
    }
}