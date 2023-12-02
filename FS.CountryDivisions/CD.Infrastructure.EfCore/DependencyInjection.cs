using CD.Application;
using CD.Domain.Repositories;
using CD.Infrastructure.EfCore.Persistence;
using CD.Infrastructure.EfCore.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Application.Interfaces;

namespace CD.Infrastructure.EfCore;

public static class DependencyInjection
{
    public static void AddCountryDivisionModule(this IServiceCollection services, string connectionString)
    {
        services.SetupApplication();

        services.SetupInfrastructure(connectionString);
    }

    public static void SetupInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CountryDivisionDbContext>(_ =>
        {
            _.UseSqlServer(connectionString);
        });

        services.AddScoped<IPermissionExposer, CountryDivisionManagementPermissionExposer>();

        services.AddScoped<ICountryDivisionRepository, CountryDivisionRepository>();
    }
}