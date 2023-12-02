using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VG.Domain.Models;

namespace VG.Application;
public static class DependencyInjection
{
    public static void SetupVarietyGroupApplication(this IServiceCollection services)
    {
        services.AddScoped<VarietyGroupFactory>();
        services.AddScoped<VarietyFactory>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
