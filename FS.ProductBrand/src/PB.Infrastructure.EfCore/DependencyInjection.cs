using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PB.Application;
using PB.Domain.Repositories;
using PB.Infrastructure.EfCore.Persistence;
using PB.Infrastructure.EfCore.Persistence.Repositories;

using SH.Application.Interfaces;

namespace PB.Infrastructure.EfCore;

public static class DependencyInjection
{
    public static void AddBrandModule(this IServiceCollection services, string connectionString)
    {
        services.SetupInfrastructure(connectionString);

        services.SetupApplication();
    }

    private static void SetupInfrastructure(this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<BrandDbContext>(_ =>
        {
            _.UseSqlServer(connectionString);
        });

        services.AddScoped<IPermissionExposer, ProductBrandManagementPermissionExposer>();

        services.AddScoped<IBrandRepository, BrandRepository>();
    }
}