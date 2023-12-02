using FaghihstoreQuery.Interfaces;
using FaghihstoreQuery.Products;
using Microsoft.Extensions.DependencyInjection;

namespace FaghihstoreQuery;

public static class DependencyInjection
{
    public static void AddQuery(this IServiceCollection services)
    {
        services.AddScoped<IProductQuery, ProductQuery>();
    }
}
