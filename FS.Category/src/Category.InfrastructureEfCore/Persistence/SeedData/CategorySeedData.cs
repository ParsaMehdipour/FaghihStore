using Category.Domain.Models;
using Category.Domain.Repositories;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Infrastructure.Extensions;

namespace Category.InfrastructureEfCore.Persistence.SeedData;

public static class CategorySeedData
{
    public static async Task HandleCategoryData(this IApplicationBuilder builder)
    {
        var scope = builder.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var categoryContext = services.GetService<CategoryDbContext>();
        await categoryContext.Database.MigrateAsync();
        await categoryContext.Database.EnsureCreatedAsync();

        var categoryRepository = services.GetRequiredService<ICategoryRepository>();

        if (await categoryRepository.IsExistsAsync(CancellationToken.None) is false)
        {
            var factory = services.GetRequiredService<CategoryFactory>();

            List<Domain.Models.Category> categories = new()
            {
                factory.Create("دیجیتال","digital",true),
                factory.Create("لوازم خانگی","home-appliances",true)
            };

            await categoryRepository.AddRangeAsync(categories, CancellationToken.None);
            await categoryRepository.SaveAsync(CancellationToken.None);

            services.SeedLogger("Category Data Seeded!");
        }
    }
}