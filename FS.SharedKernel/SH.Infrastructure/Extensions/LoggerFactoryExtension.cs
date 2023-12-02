using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;

namespace SH.Infrastructure.Extensions;

public static class LoggerFactoryExtension
{
    public static void UseSentryLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.Enrich.FromLogContext()
                                .ReadFrom.Configuration(context.Configuration);
        });

        builder.WebHost.UseSentry();
    }

    private static ILoggerFactory GetLoggerFactory(this IServiceProvider provider)
    {
        return provider.GetRequiredService<ILoggerFactory>();
    }

    public static void SeedLogger(this IServiceProvider provider, string message, LogLevel logLevel = LogLevel.Information)
    {
        provider.GetLoggerFactory().CreateLogger("Seed-Data").Log(logLevel, message);
    }

    public static void CookieLogger(this IServiceProvider provider, string message, LogLevel logLevel = LogLevel.Information)
    {
        provider.GetLoggerFactory().CreateLogger("Cookie").Log(logLevel, message);
    }
}