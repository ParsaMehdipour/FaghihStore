namespace SH.Infrastructure.Consts;

public class IdentityConsts
{
    public const string ROLE_ADMIN = "ADMIN";
    public const string ROLE_USER = "USER";

    public const string IDENTITY_TABLE_SCHEMA = "identity";

    public const string LoginPath = "/auth/login";
    //public const string LoginPath = "https://localhost:7092/register";
    public const string LogoutPath = "/auth/logout";
    public const string AccessDeniedPath = "/auth/accessdenied";

    public const string AuthenticationScheme = "FaghihStore:Auth";

    public const string CookieActivationCodeKey = "ActivationCode";
}