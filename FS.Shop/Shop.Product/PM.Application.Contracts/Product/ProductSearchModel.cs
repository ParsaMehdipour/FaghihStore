using System;

namespace PM.Application.Contracts.Product
{
    public class ProductSearchModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid CategoryId { get; set; }
    }
}
