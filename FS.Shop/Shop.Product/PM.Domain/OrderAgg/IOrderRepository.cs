﻿using System.Collections.Generic;

using _0_Framework.Domain;

using PM.Application.Contracts.Order;

namespace PM.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<long, Order>
    {
        double GetAmountBy(long id);
        List<OrderItemViewModel> GetItems(long orderId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}