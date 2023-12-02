using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SS.Domain.Models;

namespace SS.Infrastructure.EfCore.Persistence.Configurations;

public class SitePanelSenderConfiguration : IEntityTypeConfiguration<SitePanelSender>
{
    public void Configure(EntityTypeBuilder<SitePanelSender> builder)
    {
        builder.Property(_ => _.Username).HasMaxLength(50).IsRequired();
        builder.Property(_ => _.Password).IsRequired();
    }
}