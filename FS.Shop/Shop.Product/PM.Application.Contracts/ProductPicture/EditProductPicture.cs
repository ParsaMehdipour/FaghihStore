using System;

namespace PM.Application.Contracts.ProductPicture
{
    public class EditProductPicture : CreateProductPicture
    {
        public Guid Id { get; set; }
    }
}
