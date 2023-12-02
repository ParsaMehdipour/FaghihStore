using SH.Infrastructure.Identity;

namespace SH.Infrastructure.Settings;

/// <summary>
/// bind siteSetting to project that contains list of <see cref="ProjectsUrls"/> and <see cref="IdentitySettings"/> 
/// that get their data from Configuration.
/// </summary>
public class SiteSettings
{
    public IdentitySettings IdentitySettings { get; set; }
    public List<ProjectsUrls> ProjectsUrls { get; set; }
}