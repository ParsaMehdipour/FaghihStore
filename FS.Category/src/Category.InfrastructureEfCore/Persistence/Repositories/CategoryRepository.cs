using Category.Domain.Repositories;
using SH.Infrastructure.EfCore.Repositories;
using System.Linq.Expressions;

namespace Category.InfrastructureEfCore.Persistence.Repositories;

public class CategoryRepository : EfRepository<Domain.Models.Category>, ICategoryRepository
{
    protected CategoryDbContext _categoryDbContext { get; }

    public CategoryRepository(CategoryDbContext context) : base(context)
    {
        _categoryDbContext = context;
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

    public async Task<bool> SlugExists(Guid id, string slug, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.Slug.Equals(slug), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.Slug.Equals(slug), cancellationToken);

        return isExists;
    }

    public async Task<bool> OrderNumberExists(Guid id, int orderNumber, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.OrderNumber.Equals(orderNumber), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.OrderNumber.Equals(orderNumber), cancellationToken);

        return isExists;
    }

    public async Task<bool> ParentExists(Guid id, Guid parentId, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists = true;

        if (parentId.Equals(Guid.Empty) is false)
        {
            if (isForModify is false)
                isExists = await this.IsExistsAsync(_ => _.ParentCategoryId.Equals(parentId), cancellationToken);
            else
                isExists = await this.IsExistsAsync(_ => !_.ParentCategoryId.Equals(id) && _.ParentCategoryId.Equals(parentId), cancellationToken);
        }

        return isExists;
    }
    public Domain.Models.Category GetWithRecursiveParent(Expression<Func<Domain.Models.Category, bool>> filter)
    {
        var child = Entities.FirstOrDefault(filter);
        GetParent(child);
        return child;
    }

    private void GetParent(Domain.Models.Category child)
    {
        _categoryDbContext.Entry(child).Reference(e => e.Parent).Load();

        if (child.Parent != null)
        {
            GetParent(child.Parent);
        }
    }
}
