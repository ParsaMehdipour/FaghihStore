using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB.Domain.Models;

namespace PB.Infrastructure.EfCore.Persistence.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(_ => _.Title).HasMaxLength(100).IsRequired();
        builder.Property(_ => _.Slug).HasMaxLength(100).IsRequired();

        builder.HasQueryFilter(_ => _.IsDeleted == false);
    }
}
