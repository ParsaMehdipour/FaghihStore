using Identity.Infrastructure.Persistence.SeedData;

namespace Identity.Extensions;

public static class ApplicationBuilderExtension
{
    public static async Task InitializeSeedData(this IApplicationBuilder app)
    {
        await app.HandleRoleData();
        await app.HandleApplicationUserData();
    }
}