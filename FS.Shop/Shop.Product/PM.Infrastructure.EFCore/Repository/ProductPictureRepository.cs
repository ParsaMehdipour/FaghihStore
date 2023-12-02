using PM.Domain.ProductImageAgg;

using SH.Infrastructure.EfCore.Repositories;

namespace PM.Infrastructure.EFCore.Repository
{
    public class ProductImageRepository : EfRepository<ProductImage>, IProductImageRepository
    {
        private readonly ShopContext _context;

        public ProductImageRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
    }
}