using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Category.InfrastructureEfCore.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Models.Category>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Category> builder)
    {
        builder.Property(_ => _.Title).HasMaxLength(100);

        builder.Property(_ => _.Slug).HasMaxLength(100);

        builder.HasQueryFilter(entity => entity.IsDeleted == false);

        builder.HasOne(_ => _.Parent).WithMany(_ => _.Children).HasForeignKey(_ => _.ParentCategoryId);
    }
}
