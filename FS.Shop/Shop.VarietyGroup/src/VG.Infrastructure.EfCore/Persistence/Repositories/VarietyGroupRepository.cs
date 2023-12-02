using SH.Infrastructure.EfCore.Repositories;
using VG.Domain.Repositories;

namespace VG.Infrastructure.EfCore.Persistence.Repositories;

public class VarietyGroupRepository : EfRepository<Domain.Models.VarietyGroup>, IVarietyGroupRepository
{
    public VarietyGroupRepository(VarietyGroupDbContext context) : base(context)
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
