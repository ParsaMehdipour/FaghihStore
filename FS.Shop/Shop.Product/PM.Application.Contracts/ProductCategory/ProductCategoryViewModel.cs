using System;

namespace PM.Application.Contracts.ProductCategory
{
    public class ProductCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string CreationDate { get; set; }
        public long ProductsCount { get; set; }
    }
}
