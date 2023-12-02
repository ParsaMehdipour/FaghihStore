using PM.Domain.ProductTraitAggregate;

using SH.Infrastructure.EfCore.Repositories;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace PM.Infrastructure.EFCore.Repository;
public class ProductTraitItemRepository : EfRepository<ProductTraitItem>, IProductTraitItemRepository
{
    public ProductTraitItemRepository(ShopContext context) : base(context)
    {
    }

    public async Task<bool> ValueExists(Guid id, string value, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.Value.Equals(value), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.Value.Equals(value), cancellationToken);

        return isExists;
    }
}