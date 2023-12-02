namespace PB.Domain.Models;

public class BrandFactory
{
    public Brand Create(string title, int orderNumber, string slug, bool status)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(title, nameof(title));

        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentNullException(slug, nameof(slug));

        if (orderNumber == 0)
            throw new ArgumentNullException(orderNumber.ToString(), nameof(orderNumber));

        Brand brand = new(title, orderNumber, slug, status);

        return brand;
    }
}

