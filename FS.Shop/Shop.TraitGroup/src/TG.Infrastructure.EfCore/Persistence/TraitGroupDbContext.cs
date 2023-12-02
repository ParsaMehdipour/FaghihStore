using Microsoft.EntityFrameworkCore;

using SH.Application.Interfaces;
using SH.Infrastructure.EfCore.Extensions;

using System.Reflection;

using TG.Domain.Models;

namespace TG.Infrastructure.EfCore.Persistence;

public class TraitGroupDbContext : DbContext, IApplicationDbContext
{
    public TraitGroupDbContext(DbContextOptions<TraitGroupDbContext> options) : base(options)
    {

    }

    public DbSet<TraitGroup> TraitGroups { get; set; }
    public DbSet<Trait> Traits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.AddSequentialGuidForIdConvention();
    }
}