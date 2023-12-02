using Microsoft.EntityFrameworkCore;
using SH.Application.Interfaces;
using SH.Infrastructure.EfCore.Extensions;
using System.Reflection;
using VG.Domain.Models;

namespace VG.Infrastructure.EfCore.Persistence;

public class VarietyGroupDbContext : DbContext, IApplicationDbContext
{
    public VarietyGroupDbContext(DbContextOptions<VarietyGroupDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Models.VarietyGroup> VarietyGroups { get; set; }
    public DbSet<Variety> Varieties { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.AddSequentialGuidForIdConvention();
    }
}
