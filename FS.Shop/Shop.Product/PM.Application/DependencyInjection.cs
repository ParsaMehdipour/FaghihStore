using MediatR;

using Microsoft.Extensions.DependencyInjection;

using PM.Domain.ProductAgg;
using PM.Domain.ProductTraitAggregate;

using System.Reflection;

namespace PM.Application;

public static class DependencyInjection
{
    public static void SetupApplication(this IServiceCollection services)
    {
        services.AddScoped<ProductFactory>();
        services.AddScoped<ProductTraitItemFactory>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}