using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VG.Infrastructure.EfCore.Persistence.Configurations;

public class VarietyConfiguration : IEntityTypeConfiguration<VG.Domain.Models.Variety>
{
    public void Configure(EntityTypeBuilder<VG.Domain.Models.Variety> builder)
    {
        builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();

        builder.Property(_ => _.ColorCode).HasMaxLength(50);

        builder.Property(_ => _.Size).HasMaxLength(50);

        builder.Property(_ => _.VarietyGroupId).IsRequired();

        builder.HasQueryFilter(entity => entity.IsDeleted == false);

        builder.HasOne(_ => _.VarietyGroup).WithMany(_ => _.ProductVarieties).HasForeignKey(_ => _.VarietyGroupId);

    }
}
