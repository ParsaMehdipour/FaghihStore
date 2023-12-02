using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SS.Domain.Models;

namespace SS.Infrastructure.EfCore.Persistence.Configurations;
public class SiteSettingConfiguration : IEntityTypeConfiguration<SiteSetting>
{
    public void Configure(EntityTypeBuilder<SiteSetting> builder)
    {
        builder.Property(_ => _.Title).HasMaxLength(200).IsRequired();
        builder.Property(_ => _.Description).HasMaxLength(1000);
        builder.Property(_ => _.Favicon).HasMaxLength(50);
        builder.Property(_ => _.Logo).HasMaxLength(50);
        builder.Property(_ => _.MobilePhoneNumber).HasMaxLength(15);
        builder.Property(_ => _.Phone).HasMaxLength(15);

        builder.Ignore(_ => _.IsDeleted);
    }
}