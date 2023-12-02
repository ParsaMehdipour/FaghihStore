using Identity.Infrastructure.Persistence.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Role.Domain.Models;

using SH.Application.Interfaces;
using SH.Infrastructure.EfCore.Extensions;

using User.Domain.Models;

namespace Identity.Infrastructure.Persistence;

public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleConfiguration());

        //set schema to identity default tables
        #region Tables

        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole", IdentityConsts.IDENTITY_TABLE_SCHEMA);

        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim", IdentityConsts.IDENTITY_TABLE_SCHEMA);

        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin", IdentityConsts.IDENTITY_TABLE_SCHEMA);

        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken", IdentityConsts.IDENTITY_TABLE_SCHEMA);

        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim", IdentityConsts.IDENTITY_TABLE_SCHEMA);

        #endregion

        builder.AddSequentialGuidForIdConvention();
    }
}