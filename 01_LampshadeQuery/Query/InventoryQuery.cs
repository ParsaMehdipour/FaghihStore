using _01_LampshadeQuery.Contracts.Inventory;
using Inventory.Infrastructure.EFCore.Persistence;
using PM.Infrastructure.EFCore;
using System;

namespace _01_LampshadeQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            //var inventory = _inventoryContext.Inventories.FirstOrDefault(x => x.ProductId == command.ProductId);
            //if (inventory == null || inventory.CalculateCurrentCount() < command.Count)
            //{
            //    var product = _shopContext.Products.Select(x => new { x.Id, x.TitlePersian })
            //        .FirstOrDefault(x => x.Id == command.ProductId);
            //    return new StockStatus
            //    {
            //        IsStock = false,
            //        ProductName = product?.TitlePersian
            //    };
            //}

            //return new StockStatus
            //{
            //    IsStock = true
            //};

            throw new NotImplementedException();
        }
    }
}