using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Infrastructure.Extensions;

namespace Inventory.Infrastructure.EFCore.Persistence.SeedData;

public static class InventorySeedData
{
    public static async Task HandleInventoryData(this IApplicationBuilder builder)
    {
        await using var scope = builder.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var inventoryContext = services.GetService<InventoryContext>();
        await inventoryContext.Database.MigrateAsync();
        await inventoryContext.Database.EnsureCreatedAsync();

        services.SeedLogger("Inventory Data Seeded!");

        await Task.CompletedTask;
    }
}