using PB.Domain.Models;
using SH.Domain.Interfaces;

namespace PB.Domain.Repositories
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        public Task<bool> OrderNumberBeUnique(Guid id, int orderNumber, bool isForModify, CancellationToken cancellationToken);
        public Task<bool> TitleBeUnique(Guid id, string title, bool isForModify, CancellationToken cancellationToken);
        public Task<bool> SlugBeUnique(Guid id, string slug, bool isForModify, CancellationToken cancellationToken);
    }
}
