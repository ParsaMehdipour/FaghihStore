using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Application;
using SH.Application.Interfaces;
using SH.Domain.Interfaces;
using SH.Infrastructure.EfCore;
using SH.Infrastructure.EfCore.Repositories;
using SH.Infrastructure.Policies;
using SH.Infrastructure.Services;

namespace SH.Infrastructure;

public static class DependencyInjection
{
    public static void AddSharedKernel(this IServiceCollection services, string connectionStrings)
    {
        services.SetupInfrastructure(connectionStrings);

        services.SetupApplication();
    }

    internal static void SetupInfrastructure(this IServiceCollection services, string connectionStrings)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IQueryRepository<>), typeof(EfQueryRepository<>));

        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IFileUploaderService, FileUploaderService>();

        services.AddScoped<ICookieHelper, CookieHelper>();
        services.AddSingleton<IHttpHeaderHelper, HttpHeaderHelper>();

        services.AddScoped<IFileUploaderService, FileUploaderService>();

        services.AddScoped<ISmsService, SmsService>();

        services.AddSingleton<ClientPolicy>();

        services.AddDbContext<ApplicationDbContext>(_ =>
        {
            _.UseSqlServer(connectionStrings);
        });

        services.AddHttpClient();

        services.AddHttpClient<HttpClientService>("BaseHttpClientService").AddPolicyHandler(request => request.Method != HttpMethod.Get ?
            new ClientPolicy().ImmediateHttpRetry :
            new ClientPolicy().LinearHttpRetry);
    }
}