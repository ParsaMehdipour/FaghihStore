using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

namespace PM.Infrastructure.EFCore.Persistence.SeedData;

public static class ShopSeedData
{
    public static async Task HandleShopData(this IApplicationBuilder builder)
    {
        var scope = builder.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var shopContext = services.GetService<ShopContext>();
        await shopContext.Database.MigrateAsync();
        await shopContext.Database.EnsureCreatedAsync();
    }
}