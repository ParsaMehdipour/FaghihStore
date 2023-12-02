using System;

namespace PM.Application.Contracts.Product
{
    public class EditProduct : CreateProduct
    {
        public Guid Id { get; set; }
    }
}