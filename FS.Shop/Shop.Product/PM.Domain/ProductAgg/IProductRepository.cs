using SH.Domain.Interfaces;

namespace PM.Domain.ProductAgg;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product> GetIncludeThumbnailImageAsync(Guid id, CancellationToken cancellationToken);
}