using _0_Framework.Domain;

using System;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public class ColleagueDiscount : EntityBase
    {
        public Guid ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public bool IsRemved { get; private set; }

        public ColleagueDiscount(Guid productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            IsRemved = false;
        }

        public void Edit(Guid productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
        }

        public void Remove()
        {
            IsRemved = true;
        }

        public void Restore()
        {
            IsRemved = false;
        }
    }
}
