using Category.Domain.Models;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Category.Application;

public static class DependencyInjection
{
    public static void SetupCategoryApplication(this IServiceCollection services)
    {
        services.AddScoped<CategoryFactory>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
