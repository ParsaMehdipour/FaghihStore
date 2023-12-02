using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Infrastructure.Extensions;

using TG.Domain.Models;
using TG.Domain.Repositories;

namespace TG.Infrastructure.EfCore.Persistence.SeedData;
public static class TraitGroupSeedData
{
    public static async Task HandleTraitGroupData(this IApplicationBuilder builder)
    {
        var scope = builder.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var traitContext = services.GetService<TraitGroupDbContext>();
        await traitContext.Database.MigrateAsync();
        await traitContext.Database.EnsureCreatedAsync();

        var traitGroupRepository = services.GetRequiredService<ITraitGroupRepository>();

        if (await traitGroupRepository.IsExistsAsync(CancellationToken.None) is false)
        {
            var factory = services.GetRequiredService<TraitGroupFactory>();

            TraitGroup traitGroup = factory.Create("مشخصات", 0);

            await traitGroupRepository.AddAsync(traitGroup, CancellationToken.None);
            await traitGroupRepository.SaveAsync(CancellationToken.None);

            services.SeedLogger("TraitGroup Data Seeded!");

            await builder.HandleTraitData(services, traitGroup.Id);
        }
    }

    public static async Task HandleTraitData(this IApplicationBuilder builder, IServiceProvider services, Guid traitGroupId)
    {
        var traitRepository = services.GetRequiredService<ITraitRepository>();

        if (await traitRepository.IsExistsAsync(CancellationToken.None) is false)
        {
            var factory = services.GetRequiredService<TraitGroupFactory>();

            List<Trait> traits = new()
            {
                factory.CreateTrait("اندازه", 0,traitGroupId,Guid.Empty,false),
                factory.CreateTrait("رزولوشن عکس", 0,traitGroupId,Guid.Empty,false),
                factory.CreateTrait("فناوری صفحه‌نمایش", 0,traitGroupId,Guid.Empty,false),
                factory.CreateTrait("نسخه سیستم عامل", 0,traitGroupId,Guid.Empty,false),
                factory.CreateTrait("اقلام همراه", 0,traitGroupId,Guid.Empty,false),
                factory.CreateTrait("حافظه داخلی", 0,traitGroupId,Guid.Empty,false),
                factory.CreateTrait("ساختار بدنه", 0,traitGroupId,Guid.Empty,false)
            };

            await traitRepository.AddRangeAsync(traits, CancellationToken.None);
            await traitRepository.SaveAsync(CancellationToken.None);

            services.SeedLogger("Trait Data Seeded!");
        }
    }
}