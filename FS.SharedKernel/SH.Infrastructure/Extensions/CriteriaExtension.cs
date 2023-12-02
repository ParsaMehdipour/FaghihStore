using SH.Infrastructure.Criteria.Pagination;

using System.Reflection;
using System.Text;

namespace SH.Infrastructure.Extensions;

public static class CriteriaExtension
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, Pager pagination)
    {
        int skip = (pagination.CurrentPage - 1) * pagination.PageSize;
        int take = pagination.PageSize;
        return source.Skip(skip).Take(take);
    }

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> entities, string orderByQueryString)
    {
        if (!entities.Any())
            return entities;

        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return entities;
        }

        var orderParams = orderByQueryString.Trim().Split(',');

        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        StringBuilder orderQueryBuilder = new();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];

            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

            orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
        }
        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

        return entities.OrderBy(_ => _.Equals(orderQuery));
    }
}