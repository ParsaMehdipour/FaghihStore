using System;

namespace PM.Application.Contracts.ProductPicture
{
    public class ProductPictureViewModel
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public string Picture { get; set; }
        public string CreationDate { get; set; }
        public Guid ProductId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
