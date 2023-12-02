using Microsoft.EntityFrameworkCore;

using PM.Domain.ProductAgg;

using SH.Infrastructure.EfCore.Repositories;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PM.Infrastructure.EFCore.Repository;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    private readonly ShopContext _context;

    public ProductRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Product> GetIncludeThumbnailImageAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.Get(_ => _.Id.Equals(id)).Include(_ => _.Images.Where(productImage => productImage.IsThumbnail)).SingleOrDefaultAsync(cancellationToken);
    }
}