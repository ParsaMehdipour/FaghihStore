using Microsoft.AspNetCore.Authorization;

namespace Identity.Infrastructure.PolicyProviders.Permissions;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    public PermissionAuthorizationHandler() { }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User is null)
            return Task.CompletedTask;

        var permissionss = context.User.Claims.Where(x => x.Type == "Permission" &&
                                                            x.Value == requirement.Permission &&
                                                            x.Issuer == "LOCAL AUTHORITY");
        if (permissionss.Any())
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}