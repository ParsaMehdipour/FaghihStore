using PM.Domain.OrderAgg;
using PM.Domain.Services;

using System.Collections.Generic;

namespace PM.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        //private readonly IInventoryApplication _inventoryApplication;

        //public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        //{
        //    _inventoryApplication = inventoryApplication;
        //}

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            //var command = items.Select(orderItem =>
            //        new ReduceInventory(orderItem.ProductId, orderItem.Count, "خرید مشتری", orderItem.OrderId))
            //    .ToList();

            //return _inventoryApplication.Reduce(command).IsSuccedded;

            return true;
        }
    }
}