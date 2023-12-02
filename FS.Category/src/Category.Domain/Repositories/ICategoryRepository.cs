using SH.Domain.Interfaces;
using System.Linq.Expressions;

namespace Category.Domain.Repositories;

public interface ICategoryRepository : IBaseRepository<Models.Category>
{
    Task<bool> TitleExists(Guid id, string title, bool isForModify, CancellationToken cancellationToken);
    Task<bool> SlugExists(Guid id, string slug, bool isForModify, CancellationToken cancellationToken);
    Task<bool> OrderNumberExists(Guid id, int orderNumber, bool isForModify, CancellationToken cancellationToken);
    Task<bool> ParentExists(Guid id, Guid parentId, bool isForModify, CancellationToken cancellationToken);
    Domain.Models.Category GetWithRecursiveParent(Expression<Func<Domain.Models.Category, bool>> filter);
}
