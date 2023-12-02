using Microsoft.Extensions.Diagnostics.HealthChecks;

using SH.Infrastructure.Settings;

namespace ServiceHost.HealthChecks;

public class EndpointHealthChecks : IHealthCheck
{
    protected IConfiguration Configuration { get; }
    protected IHttpClientFactory _httpClientFactory { get; }
    protected List<ProjectsUrls> ProjectsUrls { get; }

    public EndpointHealthChecks(IConfiguration configuration,
        IHttpClientFactory httpClientFactory,
        IOptionsSnapshot<SiteSettings> optionsSnapshot)
    {
        Configuration = configuration;
        _httpClientFactory = httpClientFactory;
        ProjectsUrls = optionsSnapshot.Value.ProjectsUrls;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var projectUrl = ProjectsUrls.Where(_ => context.Registration.Tags.Contains(_.Project) && context.Registration.Tags.Contains("endpoints")).First();

        ArgumentNullException.ThrowIfNull(nameof(projectUrl));

        var client = _httpClientFactory.CreateClient("HealthCheckClient");
        client.BaseAddress = new(projectUrl.Url);

        var response = await client.GetAsync(string.Empty);

        return response is not null ?
            await Task.FromResult(new HealthCheckResult(
                status: HealthStatus.Healthy,
                description: $"The Project {projectUrl.Project} is Healthy")) :
            await Task.FromResult(new HealthCheckResult(
                status: HealthStatus.Unhealthy,
                description: $"The Project {projectUrl.Project} is UnHealthy : {client.BaseAddress} - {response.StatusCode}"));
    }
}