using CD.Domain.Models;
using CD.Domain.Repositories;
using SH.Infrastructure.EfCore.Repositories;

namespace CD.Infrastructure.EfCore.Persistence.Repositories;

public class CountryDivisionRepository : EfRepository<CountryDivision>, ICountryDivisionRepository
{
    public CountryDivisionRepository(CountryDivisionDbContext context) : base(context)
    {
    }

    public async Task<bool> NameIsExists(Guid id, string name, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.Name.Equals(name), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.Name.Equals(name), cancellationToken);

        return isExists;
    }

    public async Task<bool> ParentIsExists(Guid id, Guid parentId, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists = false;

        if (parentId.Equals(Guid.Empty) is false)
        {
            if (isForModify is false)
                isExists = await this.IsExistsAsync(_ => _.ParentId.Equals(parentId), cancellationToken);
            else
                isExists = await this.IsExistsAsync(_ => !_.ParentId.Equals(id) && _.ParentId.Equals(parentId), cancellationToken);
        }

        return isExists;
    }
}
