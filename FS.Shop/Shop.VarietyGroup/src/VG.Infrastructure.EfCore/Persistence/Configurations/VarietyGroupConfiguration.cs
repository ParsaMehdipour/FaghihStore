using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VG.Infrastructure.EfCore.Persistence.Configurations;

public class VarietyGroupConfiguration : IEntityTypeConfiguration<Domain.Models.VarietyGroup>
{
    public void Configure(EntityTypeBuilder<Domain.Models.VarietyGroup> builder)
    {
        builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();

        builder.HasQueryFilter(entity => entity.IsDeleted == false);

        builder.HasMany(_ => _.ProductVarieties).WithOne(_ => _.VarietyGroup).HasForeignKey(_ => _.VarietyGroupId);
    }
}
