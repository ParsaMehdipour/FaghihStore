using PM.Domain.ProductDescriptionAgg;
using SH.Infrastructure.EfCore.Repositories;

namespace PM.Infrastructure.EFCore.Repository;

public class ProductDescriptionRepository : EfRepository<ProductDescription>, IProductDescriptionRepository
{
    public ProductDescriptionRepository(ShopContext context) : base(context)
    {
    }
}
