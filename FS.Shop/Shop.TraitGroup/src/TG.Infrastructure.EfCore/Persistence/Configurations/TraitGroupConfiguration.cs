using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TG.Domain.Models;

namespace TG.Infrastructure.EfCore.Persistence.Configurations;
internal class TraitGroupConfiguration : IEntityTypeConfiguration<TraitGroup>
{
    public void Configure(EntityTypeBuilder<TraitGroup> builder)
    {
        builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();

        builder.HasQueryFilter(entity => entity.IsDeleted == false);

        builder.HasMany(_ => _.Traits)
            .WithOne(_ => _.TraitGroup)
            .HasForeignKey(_ => _.TraitGroupId);
    }
}