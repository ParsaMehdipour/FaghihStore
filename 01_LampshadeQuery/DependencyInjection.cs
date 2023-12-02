using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Contracts.Slide;
using _01_LampshadeQuery.Query;

using Microsoft.Extensions.DependencyInjection;

namespace _01_LampshadeQuery;
public static class DependencyInjection
{
    public static void AddQueryModule(this IServiceCollection services)
    {
        services.AddTransient<ISlideQuery, SlideQuery>();
        services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
        services.AddTransient<IProductQuery, ProductQuery>();
        services.AddTransient<ICartCalculatorService, CartCalculatorService>();
    }
}