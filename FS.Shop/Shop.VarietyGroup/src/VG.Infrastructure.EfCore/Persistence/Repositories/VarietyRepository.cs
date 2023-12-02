using SH.Infrastructure.EfCore.Repositories;
using VG.Domain.Models;
using VG.Domain.Repositories;

namespace VG.Infrastructure.EfCore.Persistence.Repositories;

public class VarietyRepository : EfRepository<Variety>, IVarietyRepository
{
    public VarietyRepository(VarietyGroupDbContext context) : base(context)
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

    public async Task<bool> ColorCodeExists(Guid id, string colorCode, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.ColorCode.Equals(colorCode), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.ColorCode.Equals(colorCode), cancellationToken);

        return isExists;
    }

    public async Task<bool> SizeExists(Guid id, string size, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.Size.Equals(size), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.Size.Equals(size), cancellationToken);

        return isExists;
    }
}
