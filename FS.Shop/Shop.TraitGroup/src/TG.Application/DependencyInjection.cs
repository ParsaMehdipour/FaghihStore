using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using TG.Domain.Models;

namespace TG.Application;
public static class DependencyInjection
{
    public static void SetupApplication(this IServiceCollection services)
    {
        services.AddScoped<TraitGroupFactory>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}