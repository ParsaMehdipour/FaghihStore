using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using SH.Application.Behaviors;

using System.Reflection;

namespace SH.Application;

public static class DependencyInjection
{
    public static void SetupApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(assembly);
    }
}