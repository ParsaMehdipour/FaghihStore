using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SH.Infrastructure.Extensions;
public static class SecurityExtensions
{
    public static void AddSharedCookieDataProtection(this IServiceCollection services, string applicationName, IWebHostEnvironment environment)
    {
        var currentDirectory = Directory.GetCurrentDirectory().Split("\\").SkipLast(environment.IsDevelopment() ? 2 : 1);

        services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo(string.Join("\\", currentDirectory)))
        .SetApplicationName(applicationName);
    }
}