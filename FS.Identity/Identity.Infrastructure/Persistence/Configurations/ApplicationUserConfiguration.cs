using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using User.Domain.Models;

namespace Identity.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("User", IdentityConsts.IDENTITY_TABLE_SCHEMA);

        builder.Property(_ => _.UserName).HasMaxLength(11).IsRequired();
        builder.Property(_ => _.PhoneNumber).HasMaxLength(11).IsRequired();
        builder.Property(_ => _.Email).HasMaxLength(250);
        builder.Property(_ => _.Firstname).HasMaxLength(250);
        builder.Property(_ => _.Lastname).HasMaxLength(250);
        builder.Property(_ => _.NationalCode).HasMaxLength(10);

        builder.Ignore(_ => _.TwoFactorEnabled);

        builder.HasOne(_ => _.CreatedByUser).WithMany().HasForeignKey(_ => _.CreatedBy);
        builder.HasOne(_ => _.LastModifiedByUser).WithMany().HasForeignKey(_ => _.LastModifiedBy);

        builder.HasQueryFilter(_ => _.IsDeleted == false);
    }
}