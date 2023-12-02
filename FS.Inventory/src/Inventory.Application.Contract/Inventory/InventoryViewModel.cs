namespace Inventory.Application.Contract.Inventory
{
    public class InventoryViewModel
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public Guid ProductId { get; set; }
        public double UnitPrice { get; set; }
        public bool InStock { get; set; }
        public long CurrentCount { get; set; }
        public string CreationDate { get; set; }
    }
}
