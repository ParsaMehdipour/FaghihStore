using Microsoft.Extensions.DependencyInjection;

using SS.Application.Interfaces;
using SS.Application.SiteSenders;
using SS.Application.SiteSettings;
using SS.Domain.Models;

using System.Reflection;

namespace SS.Application;

public static class DependencyInjection
{
    public static void SetupApplication(this IServiceCollection services)
    {
        services.AddScoped<SiteSettingFactory>();
        services.AddScoped<SitePanelSenderFactory>();

        services.AddScoped<ISiteSettingService, SiteSettingService>();
        services.AddScoped<ISitePanelSenderService, SitePanelSenderService>();

        services.AddScoped<ISiteSettingValidatorService, SiteSettingValidatorService>();
        services.AddScoped<ISitePanelSenderValidatorService, SitePanelSenderValidatorService>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}