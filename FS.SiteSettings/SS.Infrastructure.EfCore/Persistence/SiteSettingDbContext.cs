using Microsoft.EntityFrameworkCore;

using SH.Application.Interfaces;
using SH.Infrastructure.EfCore.Extensions;

using SS.Domain.Models;

using System.Reflection;

namespace SS.Infrastructure.EfCore.Persistence;
public class SiteSettingDbContext : DbContext, IApplicationDbContext
{
    public SiteSettingDbContext(DbContextOptions<SiteSettingDbContext> options) : base(options)
    {
    }

    public DbSet<SiteSetting> SiteSettings { get; set; }
    public DbSet<SitePanelSender> SitePanelSenders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddSequentialGuidForIdConvention();
    }
}