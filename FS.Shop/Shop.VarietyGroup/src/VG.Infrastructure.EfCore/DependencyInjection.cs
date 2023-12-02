using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Application.Interfaces;

using VG.Application;
using VG.Domain.Repositories;
using VG.Infrastructure.EfCore.Persistence;
using VG.Infrastructure.EfCore.Persistence.Repositories;

namespace VG.Infrastructure.EfCore;

public static class DependencyInjection
{
    public static void AddVarietyGroupModule(this IServiceCollection services, string connectionString)
    {
        services.SetupInfrastructure(connectionString);

        services.SetupVarietyGroupApplication();
    }

    private static void SetupInfrastructure(this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<VarietyGroupDbContext>(_ =>
        {
            _.UseSqlServer(connectionString);
        });

        services.AddScoped<IPermissionExposer, VarietyGroupManagementPermissionExposer>();

        services.AddScoped<IVarietyGroupRepository, VarietyGroupRepository>();
        services.AddScoped<IVarietyRepository, VarietyRepository>();
    }
}
