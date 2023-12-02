using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PM.Domain.ProductVarietyAggregate;

namespace PM.Infrastructure.EFCore.Mapping;

public class ProductVarietyConfiguration : IEntityTypeConfiguration<ProductVariety>
{
    public void Configure(EntityTypeBuilder<ProductVariety> builder)
    {
        builder.HasOne(_ => _.Product)
            .WithMany(_ => _.ProductVarieties)
            .HasForeignKey(_ => _.ProductId);
    }
}