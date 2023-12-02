using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PM.Domain.ProductDescriptionAgg;

namespace PM.Infrastructure.EFCore.Mapping;

public class ProductDescriptionMapping : IEntityTypeConfiguration<ProductDescription>
{
    public void Configure(EntityTypeBuilder<ProductDescription> builder)
    {
        builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();
        builder.Property(_ => _.Description).IsRequired();

        builder.HasOne(_ => _.Product)
            .WithMany(_ => _.Descriptions)
            .HasForeignKey(_ => _.ProductId);
    }
}