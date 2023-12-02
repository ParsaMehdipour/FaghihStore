using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Application.Interfaces;

using TG.Application;
using TG.Domain.Repositories;
using TG.Infrastructure.EfCore.Persistence;
using TG.Infrastructure.EfCore.Persistence.Repositories;

namespace TG.Infrastructure.EfCore;

public static class DependencyInjection
{
    public static void AddTraitGroupModule(this IServiceCollection services, string connectionString)
    {
        services.SetupApplication();

        services.SetupInfrastructure(connectionString);
    }

    internal static void SetupInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TraitGroupDbContext>(_ =>
        {
            _.UseSqlServer(connectionString);
        });

        services.AddScoped<IPermissionExposer, TraitGroupManagementPermissionExposer>();

        services.AddScoped<ITraitGroupRepository, TraitGroupRepository>();
        services.AddScoped<ITraitRepository, TraitRepository>();
    }
}