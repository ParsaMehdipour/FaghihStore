using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PB.Domain.Models;
using System.Reflection;

namespace PB.Application;

public static class DependencyInjection
{
    public static void SetupApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<BrandFactory>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
