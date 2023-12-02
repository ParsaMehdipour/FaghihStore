using CD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CD.Infrastructure.EfCore.Persistence.Configurations;

public class CountryDivisionConfiguration : IEntityTypeConfiguration<CountryDivision>
{
    public void Configure(EntityTypeBuilder<CountryDivision> builder)
    {
        builder.Property(_ => _.Name).HasMaxLength(100).IsRequired();

        builder.HasQueryFilter(entity => entity.IsDeleted == false);

        builder.HasOne(_ => _.Parent).WithMany().HasForeignKey(_ => _.ParentId);

    }
}
