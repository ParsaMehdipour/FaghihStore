using PM.Domain.ProductVarietyAggregate;
using SH.Infrastructure.EfCore.Repositories;

namespace PM.Infrastructure.EFCore.Repository;

public class ProductVarietyRepository : EfRepository<ProductVariety>, IProductVarietyRepository
{
    public ProductVarietyRepository(ShopContext context) : base(context)
    {
    }

    public bool ProductVarietyExists()
    {
        throw new System.NotImplementedException();
    }
}
