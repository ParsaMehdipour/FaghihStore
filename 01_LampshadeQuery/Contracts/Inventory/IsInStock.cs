using System;

namespace _01_LampshadeQuery.Contracts.Inventory
{
    public class IsInStock
    {
        public int Count { get; set; }
        public Guid ProductId { get; set; }
    }
}
