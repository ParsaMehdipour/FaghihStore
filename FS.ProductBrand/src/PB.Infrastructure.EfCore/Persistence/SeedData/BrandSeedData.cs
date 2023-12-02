using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PB.Domain.Models;
using PB.Domain.Repositories;

using SH.Infrastructure.Extensions;

namespace PB.Infrastructure.EfCore.Persistence.SeedData;

public static class BrandSeedData
{
    public static async Task HandleBrandData(this IApplicationBuilder builder)
    {
        var scope = builder.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var brandContext = services.GetService<BrandDbContext>();
        await brandContext.Database.MigrateAsync();
        await brandContext.Database.EnsureCreatedAsync();

        var brandRepository = services.GetRequiredService<IBrandRepository>();

        if (await brandRepository.IsExistsAsync(CancellationToken.None) is false)
        {
            var factory = services.GetRequiredService<BrandFactory>();

            List<Brand> brands = new(){
                factory.Create("اپل", 1, "اپل", true),
                factory.Create("سامسونگ", 1, "سامسونگ", true)
            };

            await brandRepository.AddRangeAsync(brands, CancellationToken.None);
            await brandRepository.SaveAsync(CancellationToken.None);

            services.SeedLogger("Brand Data Seeded!");
        }
    }
}
