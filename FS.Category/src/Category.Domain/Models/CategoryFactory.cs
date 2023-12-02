namespace Category.Domain.Models;

public class CategoryFactory
{
    public Category Create(string title, string slug, bool status)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(title, nameof(title));

        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentNullException(slug, nameof(slug));

        Category category = new Category(title, slug, status);

        return category;
    }
}
