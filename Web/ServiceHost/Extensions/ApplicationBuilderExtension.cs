using Category.InfrastructureEfCore.Persistence.SeedData;

using HealthChecks.UI.Client;

using Inventory.Infrastructure.EFCore.Persistence.SeedData;

using PB.Infrastructure.EfCore.Persistence.SeedData;

using PM.Infrastructure.EFCore.Persistence.SeedData;

using TG.Infrastructure.EfCore.Persistence.SeedData;

using VG.Infrastructure.EfCore.SeedData;

namespace ServiceHost.Extensions;

public static class ApplicationBuilderExtension
{
    public static async Task InitializeSeedData(this IApplicationBuilder app)
    {
        await app.HandleCategoryData();
        await app.HandleBrandData();
        await app.HandleTraitGroupData();
        await app.HandleVarietyGroupData();
        await app.HandleShopData();
        await app.HandleInventoryData();
    }

    public static void MapHealthChecksProject(this WebApplication app)
    {
        app.MapHealthChecks("/health/endpoints", new()
        {
            Predicate = _ => _.Tags.Contains("endpoints"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecks("/health/databases", new()
        {
            Predicate = _ => _.Tags.Contains("database"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecksUI(options =>
        {
            options.UIPath = "/health-ui";
        });
    }
}