using ServiceHost.HealthChecks;

namespace ServiceHost.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddHealthChecksProject(this IServiceCollection services, string connectionString)
    {
        services.AddHealthChecks()
                .AddCheck<EndpointHealthChecks>("Identity", tags: new[] { "endpoints", "Identity" })
                .AddCheck<EndpointHealthChecks>("ServiceHost", tags: new[] { "endpoints", "ServiceHost" })
                .AddCheck<EndpointHealthChecks>("PM.Api", tags: new[] { "endpoints", "PM.Api" })
                .AddCheck<EndpointHealthChecks>("Category.Api", tags: new[] { "endpoints", "Category.Api" })
                .AddCheck<EndpointHealthChecks>("VG.Api", tags: new[] { "endpoints", "VG.Api" })
                .AddSqlServer(connectionString, tags: new[] { "database" });

        services.AddHealthChecksUI(options =>
        {
            options.SetEvaluationTimeInSeconds(3600);
        }).AddInMemoryStorage();
    }
}