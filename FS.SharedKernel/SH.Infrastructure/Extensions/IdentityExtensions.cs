using Microsoft.AspNetCore.Identity;

namespace SH.Infrastructure.Extensions;

public static class IdentityExtensions
{
    public static string FirstErrorDescription(this IdentityResult identityResult)
    {
        return identityResult.Errors.Select(_ => _.Description).First();
    }
}