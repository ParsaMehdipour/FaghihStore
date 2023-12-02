namespace CD.Domain.Models;

public class CountryDivisionFactory
{
    public CountryDivision Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(name, nameof(name));

        CountryDivision countryDivision = new(name);

        return countryDivision;
    }
}
