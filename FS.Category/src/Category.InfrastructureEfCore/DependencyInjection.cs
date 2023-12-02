using Category.Application;
using Category.Domain.Repositories;
using Category.InfrastructureEfCore.Persistence;
using Category.InfrastructureEfCore.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Application.Interfaces;

namespace Category.InfrastructureEfCore;

public static class DependencyInjection
{
    public static void AddCategoryModule(this IServiceCollection services, string connectionString)
    {
        services.SetupInfrastructure(connectionString);

        services.SetupCategoryApplication();
    }

    public static void SetupInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CategoryDbContext>(_ =>
        {
            _.UseSqlServer(connectionString);
        });

        services.AddTransient<IPermissionExposer, CategoryManagementPermissionExposer>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
