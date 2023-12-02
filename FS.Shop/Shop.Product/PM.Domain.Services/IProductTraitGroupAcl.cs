namespace PM.Domain.Services;

public interface IProductTraitGroupAcl
{
    Task<List<(Guid traitId, string trait, string traitGroup)>> GetTraitsById(Guid[] traitsId, CancellationToken cancellationToken);
}