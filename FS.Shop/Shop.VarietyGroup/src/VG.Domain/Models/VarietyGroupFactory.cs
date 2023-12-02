namespace VG.Domain.Models;

public class VarietyGroupFactory
{
    public VarietyGroup Create(string title)
    {
        VarietyGroup varietyGroup = new VarietyGroup(title: title);

        return varietyGroup;
    }
}
