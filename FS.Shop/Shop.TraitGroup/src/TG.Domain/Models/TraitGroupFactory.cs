namespace TG.Domain.Models;

public class TraitGroupFactory
{
    public TraitGroup Create(string title, int orderNumber)
    {
        ArgumentException.ThrowIfNullOrEmpty(title);

        TraitGroup traitGroup = new(title, orderNumber);

        return traitGroup;
    }

    public Trait CreateTrait(string title, int orderNumber, Guid traitGroupId, Guid categoryId, bool hasFilterAbility)
    {
        ArgumentException.ThrowIfNullOrEmpty(title);

        Trait trait = new(title, orderNumber, traitGroupId, categoryId, hasFilterAbility);

        return trait;
    }
}