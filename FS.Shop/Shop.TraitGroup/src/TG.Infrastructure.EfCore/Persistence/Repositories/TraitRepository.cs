using SH.Infrastructure.EfCore.Repositories;

using TG.Domain.Models;
using TG.Domain.Repositories;

namespace TG.Infrastructure.EfCore.Persistence.Repositories;

public class TraitRepository : EfRepository<Trait>, ITraitRepository
{
    protected ITraitGroupRepository _traitGroupRepository { get; set; }

    public TraitRepository(TraitGroupDbContext context,
        ITraitGroupRepository traitGroupRepository) : base(context)
    {
        _traitGroupRepository = traitGroupRepository;
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

    public async Task<bool> TraitGroupIdExists(Guid traitGroupId, CancellationToken cancellationToken)
    {
        return await _traitGroupRepository.IsExistsAsync(_ => _.Id.Equals(traitGroupId), cancellationToken);
    }

    public Task<bool> CategoryIdExists(Guid categoryId, CancellationToken cancellationToken)
    {
        //TODO: must be implement by Api Service to call Category module.
        return Task.FromResult(true);
    }
}