using CD.Domain.Models;
using SH.Domain.Interfaces;

namespace CD.Domain.Repositories;

public interface ICountryDivisionRepository : IBaseRepository<CountryDivision>
{
    public Task<bool> NameIsExists(Guid id, string name, bool isForModify, CancellationToken cancellationToken);
    public Task<bool> ParentIsExists(Guid id, Guid parentId, bool isForModify, CancellationToken cancellationToken);
}
