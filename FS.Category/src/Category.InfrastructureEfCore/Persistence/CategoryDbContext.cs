using Microsoft.EntityFrameworkCore;
using SH.Application.Interfaces;
using SH.Infrastructure.EfCore.Extensions;
using System.Reflection;

namespace Category.InfrastructureEfCore.Persistence;

public class CategoryDbContext : DbContext, IApplicationDbContext
{

    public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Models.Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.AddSequentialGuidForIdConvention();
    }
}
