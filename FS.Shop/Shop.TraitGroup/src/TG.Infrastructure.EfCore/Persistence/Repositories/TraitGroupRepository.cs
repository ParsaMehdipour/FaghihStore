using SH.Infrastructure.EfCore.Repositories;

using TG.Domain.Models;
using TG.Domain.Repositories;

namespace TG.Infrastructure.EfCore.Persistence.Repositories;

public class TraitGroupRepository : EfRepository<TraitGroup>, ITraitGroupRepository
{
    public TraitGroupRepository(TraitGroupDbContext context) : base(context)
    {
    }

    public async Task<bool> TitleExists(Guid id, string title, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.Title.Equals(title), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.Title.Equals(title), cancellationToken);

        return isExists;
    }
}