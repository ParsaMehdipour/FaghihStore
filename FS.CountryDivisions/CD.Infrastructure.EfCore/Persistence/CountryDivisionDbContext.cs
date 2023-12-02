using CD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using SH.Application.Interfaces;
using SH.Infrastructure.EfCore.Extensions;
using System.Reflection;

namespace CD.Infrastructure.EfCore.Persistence;

public class CountryDivisionDbContext : DbContext, IApplicationDbContext
{
    public CountryDivisionDbContext(DbContextOptions<CountryDivisionDbContext> options) : base(options)
    {
    }

    public DbSet<CountryDivision> CountryDivisions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.AddSequentialGuidForIdConvention();
    }
}
