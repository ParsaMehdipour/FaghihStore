using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TG.Domain.Models;

namespace TG.Infrastructure.EfCore.Persistence.Configurations;

internal class TraitConfiguration : IEntityTypeConfiguration<Trait>
{
    public void Configure(EntityTypeBuilder<Trait> builder)
    {
        builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();

        builder.HasQueryFilter(entity => entity.IsDeleted == false);

        builder.HasOne(_ => _.TraitGroup)
            .WithMany(_ => _.Traits)
            .HasForeignKey(_ => _.TraitGroupId);
    }
}