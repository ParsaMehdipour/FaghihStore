using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Infrastructure.Extensions;

using VG.Domain.Models;
using VG.Domain.Repositories;
using VG.Infrastructure.EfCore.Persistence;

namespace VG.Infrastructure.EfCore.SeedData;

public static class VarietyGroupSeedData
{
    public static async Task HandleVarietyGroupData(this IApplicationBuilder builder)
    {
        await using var scope = builder.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var varietyGroupContext = services.GetService<VarietyGroupDbContext>();
        await varietyGroupContext.Database.MigrateAsync();
        await varietyGroupContext.Database.EnsureCreatedAsync();

        var varietyGroupRepository = services.GetRequiredService<IVarietyGroupRepository>();

        if (await varietyGroupRepository.Get().AnyAsync(CancellationToken.None) is false)
        {
            var factory = services.GetRequiredService<VarietyGroupFactory>();

            List<Domain.Models.VarietyGroup> varietyGroups = new()
            {
                factory.Create("رنگ"),
                factory.Create("سایز"),
            };

            foreach (var varietyGroup in varietyGroups)
            {
                varietyGroupRepository.Add(varietyGroup);
            }

            await varietyGroupRepository.SaveAsync(CancellationToken.None);
        }

        services.SeedLogger("VarietyGroup Data Seeded!");

        await Task.CompletedTask;
    }
}