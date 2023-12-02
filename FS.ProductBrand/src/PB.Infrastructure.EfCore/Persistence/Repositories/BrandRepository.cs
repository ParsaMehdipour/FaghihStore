using PB.Domain.Models;
using PB.Domain.Repositories;
using SH.Infrastructure.EfCore.Repositories;

namespace PB.Infrastructure.EfCore.Persistence.Repositories;

public class BrandRepository : EfRepository<Brand>, IBrandRepository
{
    public BrandRepository(BrandDbContext context) : base(context)
    {

    }

    public async Task<bool> OrderNumberBeUnique(Guid id, int orderNumber, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.OrderNumber.Equals(orderNumber), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.OrderNumber.Equals(orderNumber), cancellationToken);

        return isExists;
    }

    public async Task<bool> TitleBeUnique(Guid id, string title, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.Title.Equals(title), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.Title.Equals(title), cancellationToken);
        return isExists;

    }

    public async Task<bool> SlugBeUnique(Guid id, string slug, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.Slug.Equals(slug), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.Slug.Equals(slug), cancellationToken);
        return isExists;
    }

}
