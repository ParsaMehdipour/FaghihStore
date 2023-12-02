namespace Role.Domain.Models;

public class RoleFactory
{
    public ApplicationRole Create(string name, string displayName, Guid? createdBy)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(name, nameof(name));

        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentNullException(displayName, nameof(displayName));

        ApplicationRole role = new(name, displayName, createdBy);

        return role;
    }
}
