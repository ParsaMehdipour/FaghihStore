using System.Collections.Generic;

using PM.Domain.OrderAgg;

namespace PM.Domain.Services
{
    public interface IShopInventoryAcl
    {
        bool ReduceFromInventory(List<OrderItem> items);
    }
}