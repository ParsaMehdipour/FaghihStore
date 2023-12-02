using System;

namespace PM.Application.Contracts.ProductCategory
{
    public class EditProductCategory : CreateProductCategory
    {
        public Guid Id { get; set; }
    }
}
