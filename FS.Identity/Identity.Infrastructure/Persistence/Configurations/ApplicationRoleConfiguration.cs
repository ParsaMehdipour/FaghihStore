using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<Role.Domain.Models.ApplicationRole>
{
    public void Configure(EntityTypeBuilder<Role.Domain.Models.ApplicationRole> builder)
    {
        builder.ToTable("Role", IdentityConsts.IDENTITY_TABLE_SCHEMA);


        builder.Property(_ => _.Name).HasMaxLength(50).IsRequired();
        builder.Property(_ => _.DisplayName).HasMaxLength(50).IsRequired();

        builder.HasQueryFilter(entity => entity.IsDeleted == false);
    }
}
