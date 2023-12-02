using VG.Domain.Enums;

namespace VG.Domain.Models;

public class VarietyFactory
{
    public Variety Create(string title, string colorCode, string size, BoxType boxType, Guid varietyGroupId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(title, nameof(title));

        if (string.IsNullOrWhiteSpace(colorCode) && string.IsNullOrWhiteSpace(size))
            throw new ArgumentNullException();

        Variety variety = new Variety(title, colorCode, size, boxType, varietyGroupId);

        return variety;

    }
}
