using Microsoft.EntityFrameworkCore;

using PB.Domain.Models;

using SH.Application.Interfaces;
using SH.Infrastructure.EfCore.Extensions;

using System.Reflection;

namespace PB.Infrastructure.EfCore.Persistence;

public class BrandDbContext : DbContext, IApplicationDbContext
{
    public BrandDbContext(DbContextOptions<BrandDbContext> options) : base(options)
    {
    }

    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddSequentialGuidForIdConvention();
    }
}