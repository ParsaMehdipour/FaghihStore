using System;

namespace PM.Application.Contracts.Order
{
    public class OrderItemViewModel
    {
        public long Id { get; set; }
        public Guid ProductId { get; set; }
        public string Product { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public int DiscountRate { get; set; }
        public long OrderId { get; set; }
    }
}