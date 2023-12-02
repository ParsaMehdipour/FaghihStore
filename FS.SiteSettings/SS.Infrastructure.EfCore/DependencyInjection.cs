using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Application.Interfaces;

using SS.Application;
using SS.Domain.Repositories;
using SS.Infrastructure.EfCore.Persistence;
using SS.Infrastructure.EfCore.Persistence.Repositories;

namespace SS.Infrastructure.EfCore;

public static class DependencyInjection
{
    public static void AddSiteSettingModule(this IServiceCollection services, string connectionString)
    {
        services.SetupInfrastructure(connectionString);

        services.SetupApplication();
    }

    internal static void SetupInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<SiteSettingDbContext>(_ =>
        {
            _.UseSqlServer(connectionString);
        });

        services.AddScoped<IPermissionExposer, SiteSettingManagementPermissionExposer>();

        services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
        services.AddScoped<ISitePanelSenderRepository, SitePanelSenderRepository>();
    }
}