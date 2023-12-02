using Microsoft.EntityFrameworkCore;

using PM.Domain.OrderAgg;
using PM.Domain.ProductAgg;
using PM.Domain.ProductCategoryAgg;
using PM.Domain.ProductDescriptionAgg;
using PM.Domain.ProductImageAgg;
using PM.Domain.ProductTraitAggregate;
using PM.Domain.ProductVarietyAggregate;
using PM.Domain.SlideAgg;
using PM.Infrastructure.EFCore.Mapping;

namespace PM.Infrastructure.EFCore
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTraitItem> ProductTraitItems { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductVariety> ProductVarieties { get; set; }
        public DbSet<ProductImage> ProductPictures { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ProductCategory>();

            var assembly = typeof(ProductMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}