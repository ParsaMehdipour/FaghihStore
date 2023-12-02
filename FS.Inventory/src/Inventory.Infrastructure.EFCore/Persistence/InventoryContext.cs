using Inventory.Infrastructure.EFCore.Persistence.Mappings;

using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.EFCore.Persistence
{
    public class InventoryContext : DbContext
    {
        public DbSet<Domain.Models.Inventory> Inventories { get; set; }
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
