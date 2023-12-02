using _0_Framework.Application;

using PM.Application.Contracts.Product;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.Contract.Inventory
{
    public class CreateInventory
    {
        //[Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public Guid ProductId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public double UnitPrice { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
