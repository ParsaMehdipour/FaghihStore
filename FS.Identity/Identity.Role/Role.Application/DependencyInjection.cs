using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Role.Domain.Models;

using System.Reflection;

namespace Role.Application;

public static class DependencyInjection
{
    public static void SetupRoleApplication(this IServiceCollection services)
    {
        services.AddScoped<RoleFactory>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}