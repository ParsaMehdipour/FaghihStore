using Microsoft.Extensions.Hosting;

using SH.Infrastructure.Consts;

namespace SH.Infrastructure.Extensions;

public static class HostExtensions
{
    public static bool IsDevelopmentOnServer(this IHostEnvironment environment)
    {
        return environment.EnvironmentName == HostEnvironmentConsts.DevelopmentOnServer;
    }
}