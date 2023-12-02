using Inventory.Application;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.EFCore.Persistence;
using Inventory.Infrastructure.EFCore.Persistence.Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Application.Interfaces;

namespace Inventory.Infrastructure.EFCore;

public static class DependencyInjection
{
    public static void AddInventoryModule(this IServiceCollection services, string connectionString)
    {
        services.SetupApplication();

        services.SetupInfrastructure(connectionString);
    }

    public static void SetupInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IInventoryRepository, InventoryRepository>();

        services.AddScoped<IPermissionExposer, InventoryManagementPermissionExposer>();

        services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
    }
}