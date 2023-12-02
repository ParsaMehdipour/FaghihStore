using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PM.Application;
using PM.Domain.ProductAgg;
using PM.Domain.ProductCategoryAgg;
using PM.Domain.ProductDescriptionAgg;
using PM.Domain.ProductImageAgg;
using PM.Domain.ProductTraitAggregate;
using PM.Domain.ProductVarietyAggregate;
using PM.Domain.Services;
using PM.Domain.SlideAgg;
using PM.Infrastructure.EFCore.Repository;
using PM.Infrastructure.ModulesAcl;

using SH.Application.Interfaces;

namespace PM.Infrastructure.EFCore;
public static class DependencyInjection
{
    public static void AddProductModule(this IServiceCollection services, string connectionString)
    {
        services.SetupApplication();

        services.SetupInfrastructure(connectionString);
    }

    public static void SetupInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IPermissionExposer, ShopManagementPermissionExposer>();

        //TODO: all of injection must be change to Scoped Lifetime.
        services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddTransient<IProductImageRepository, ProductImageRepository>();

        services.AddTransient<ISlideRepository, SlideRepository>();

        services.AddTransient<IProductDescriptionRepository, ProductDescriptionRepository>();

        services.AddTransient<IProductVarietyRepository, ProductVarietyRepository>();

        services.AddScoped<IProductTraitItemRepository, ProductTraitItemRepository>();
        services.AddScoped<IProductTraitGroupAcl, ProductTraitGroupAcl>();

        services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
    }
}