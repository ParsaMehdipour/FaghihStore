using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Persistence.Repositories;
using Identity.Infrastructure.PolicyProviders.Permissions;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Role.Application;
using Role.Domain.Models;
using Role.Domain.Repositories;

using SH.Application.Interfaces;
using SH.Infrastructure.Extensions;
using SH.Infrastructure.Services;
using SH.Infrastructure.Settings;

using User.Application;
using User.Domain.Models;
using User.Domain.Repositories;

namespace Identity.Infrastructure;

public static class DependencyInjection
{
    public static void AddIdentity(this IServiceCollection services, SiteSettings settings, string connectionString, IWebHostEnvironment environment)
    {
        services.SetupRoleApplication();

        services.SetupUserApplication();

        services.SetupInfrastructure(settings, connectionString, environment);
    }

    internal static void SetupInfrastructure(this IServiceCollection services, SiteSettings settings,
        string connectionString, IWebHostEnvironment environment)
    {
        services.AddDbContext<IdentityContext>(_ =>
        {
            _.UseSqlServer(connectionString);
        });

        services.AddIdentity<ApplicationUser, ApplicationRole>(_ =>
            {
                //Lockout
                _.Lockout.AllowedForNewUsers = settings.IdentitySettings.AllowedForNewUsers;
                _.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(settings.IdentitySettings.DefaultLockoutTimeSpanFromMinutes);
                _.Lockout.MaxFailedAccessAttempts = settings.IdentitySettings.MaxFailedAccessAttempts;

                //Password
                _.Password.RequireDigit = settings.IdentitySettings.RequireDigit;
                _.Password.RequireLowercase = settings.IdentitySettings.RequireLowercase;
                _.Password.RequireNonAlphanumeric = settings.IdentitySettings.RequireNonAlphanumeric;
                _.Password.RequireUppercase = settings.IdentitySettings.RequireUppercase;
                _.Password.RequiredLength = settings.IdentitySettings.RequiredLength;
                _.Password.RequiredUniqueChars = settings.IdentitySettings.RequiredUniqueChars;

                //Signin
                _.SignIn.RequireConfirmedAccount = settings.IdentitySettings.RequireConfirmedAccount;
                _.SignIn.RequireConfirmedEmail = settings.IdentitySettings.RequireConfirmedEmail;
                _.SignIn.RequireConfirmedPhoneNumber = settings.IdentitySettings.RequireConfirmedPhoneNumber;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        var identityProjectUrl = settings.ProjectsUrls.FirstOrDefault(_ => _.Project == "Identity").Url;

        services.ConfigureApplicationCookie(options =>
        {
            options.Events = new()
            {
                OnRedirectToLogin = context =>
                {
                    context.Response.Redirect($"{identityProjectUrl}/register?callbackurl={context.Request.GetFullUrl()}");
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = context =>
                {
                    context.Response.Redirect($"{identityProjectUrl}/accessDenied?statusCode={context.Response.StatusCode}");
                    return Task.CompletedTask;
                }
            };
            options.CookieManager = new CookieManager(environment);
        });

        services.AddSharedCookieDataProtection("IdentitySharedCookie", environment);

        services.AddScoped<IAuthManager, AuthManager>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddTransient<IPermissionExposer, UserManagementPermissionExposer>();
    }

    public static void AddIdentityAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }
}