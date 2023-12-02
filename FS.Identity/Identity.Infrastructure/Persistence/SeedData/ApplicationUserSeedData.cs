using IdentityModel;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SH.Infrastructure.Extensions;

using System.Security.Claims;

using User.Domain.Enums;
using User.Domain.Models;

namespace Identity.Infrastructure.Persistence.SeedData;

public static class ApplicationUserSeedData
{
    public static async Task HandleApplicationUserData(this IApplicationBuilder builder)
    {
        await using var scope = builder.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var userFactory = services.GetRequiredService<ApplicationUserFactory>();

        try
        {
            var identityContext = services.GetService<IdentityContext>();
            await identityContext.Database.MigrateAsync();
            await identityContext.Database.EnsureCreatedAsync();


            await CreateAdmin(userManager, userFactory, services);
            await CreateUser(userManager, userFactory, services);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Identity Seed has error : {0}", exception.Message);
        }
    }

    private static async Task CreateAdmin(UserManager<ApplicationUser> userManager, ApplicationUserFactory userFactory,
        IServiceProvider services)
    {
        var users = await userManager.GetUsersInRoleAsync(IdentityConsts.ROLE_ADMIN);

        if (users.Any() is false)
        {
            users.Add(userFactory.CreateAdmin("آرمین", "حبیبی", "09106692003", "2130862101", "devwitharmin@gmail.com", Status.Unblock, null));
            users.Add(userFactory.CreateAdmin("پارسا", "مهدی پور", "09122502978", "2130862102", "mehdipourparsa@gmail.com", Status.Unblock, null));
            users.Add(userFactory.CreateAdmin("مجتبی", "فقیه نیا", "09114409945", "2130862103", "mfaghih63@gmail.com", Status.Unblock, null));

            await Create(userManager, users, IdentityConsts.ROLE_ADMIN, "12345678");

            services.SeedLogger("Admin Data Seeded!");
        }
    }

    private static async Task CreateUser(UserManager<ApplicationUser> userManager, ApplicationUserFactory userFactory,
        IServiceProvider services)
    {
        var users = await userManager.GetUsersInRoleAsync(IdentityConsts.ROLE_USER);

        if (users.Any() is false)
        {
            users.Add(userFactory.Create("09037536873"));
            users.Add(userFactory.Create("09051893867"));
            users.Add(userFactory.Create("09301908032"));

            await Create(userManager, users, IdentityConsts.ROLE_USER, "87654321");

            services.SeedLogger("User Data Seeded!");
        }
    }

    private static async Task Create(UserManager<ApplicationUser> userManager, IList<ApplicationUser> users, string role, string password)
    {
        IdentityResult result;

        foreach (var user in users)
        {
            result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                result = await userManager.AddToRoleAsync(user, role);

                await AddUserClaims(userManager, user);
            }
        }
    }

    private static async Task AddUserClaims(UserManager<ApplicationUser> userManager, ApplicationUser user)
    {
        await userManager.AddClaimsAsync(user, new Claim[]
       {
                 new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                            new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber)
       });
    }
}