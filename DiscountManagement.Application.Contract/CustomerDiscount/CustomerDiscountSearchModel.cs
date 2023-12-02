using System;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountSearchModel
    {
        public Guid ProductId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
