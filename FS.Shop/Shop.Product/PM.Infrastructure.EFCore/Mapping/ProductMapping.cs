using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PM.Domain.ProductAgg;

namespace PM.Infrastructure.EFCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TitlePersian).HasMaxLength(255).IsRequired();
            builder.Property(x => x.TitleEnglish).HasMaxLength(255).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(500).IsRequired();
            builder.Property(_ => _.WarrantyDescription).HasMaxLength(50);

            builder.HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

            builder.HasMany(_ => _.Descriptions)
                .WithOne(_ => _.Product)
                .HasForeignKey(_ => _.ProductId);

            builder.HasMany(_ => _.ProductVarieties)
                .WithOne(_ => _.Product)
                .HasForeignKey(_ => _.ProductId);
        }
    }
}
