using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PM.Domain.ProductTraitAggregate;

namespace PM.Infrastructure.EFCore.Mapping;

public class ProductTraitItemConfiguration : IEntityTypeConfiguration<ProductTraitItem>
{
    public void Configure(EntityTypeBuilder<ProductTraitItem> builder)
    {
        builder.Property(property => property.Value)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(_ => _.Product)
            .WithMany(_ => _.ProductTraitItems)
            .HasForeignKey(_ => _.ProductId);
    }
}
