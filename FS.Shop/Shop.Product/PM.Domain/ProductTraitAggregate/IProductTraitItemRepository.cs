using SH.Domain.Interfaces;

namespace PM.Domain.ProductTraitAggregate;

public interface IProductTraitItemRepository : IBaseRepository<ProductTraitItem>
{
    Task<bool> ValueExists(Guid id, string value, bool isForModify, CancellationToken cancellationToken);
}