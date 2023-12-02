using SH.Application.Interfaces;
using SH.Application.Models;

namespace Inventory.Infrastructure.EFCore;

public class InventoryManagementPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<RoleClaimsViewModel>> Expose()
    {
        return new Dictionary<string, List<RoleClaimsViewModel>>
        {
            {
                "Inventory", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.INVENTORY_View, Type = "Permission"},
                    new(){Value = Permissions.INVENTORY_Create, Type = "Permission"},
                    new(){Value = Permissions.INVENTORY_Edit, Type = "Permission"},
                    new(){Value = Permissions.INVENTORY_Increase, Type = "Permission"},
                    new(){Value = Permissions.INVENTORY_Reduce, Type = "Permission"},
                }
            },
            {
                "InventoryOperation", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.INVENTORYOPERATION_Logs, Type = "Permission"},
                }
            }
        };
    }

    public static class Permissions
    {
        public const string INVENTORY_View = "Permissions.Inventories.View";
        public const string INVENTORY_Create = "Permissions.Inventories.Create";
        public const string INVENTORY_Edit = "Permissions.Inventories.Edit";
        public const string INVENTORY_Increase = "Permissions.Inventories.Increase";
        public const string INVENTORY_Reduce = "Permissions.Inventories.Reduce";

        public const string INVENTORYOPERATION_Logs = "Permissions.InventoryOperations.Logs";
    }
}