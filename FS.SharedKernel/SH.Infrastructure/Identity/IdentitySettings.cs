namespace SH.Infrastructure.Identity;

public class IdentitySettings
{
    //Lockout
    public bool AllowedForNewUsers { get; set; }
    public int DefaultLockoutTimeSpanFromMinutes { get; set; }
    public int MaxFailedAccessAttempts { get; set; }

    //Password
    public bool RequireDigit { get; set; }
    public bool RequireLowercase { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public bool RequireUppercase { get; set; }
    public int RequiredLength { get; set; }
    public int RequiredUniqueChars { get; set; }

    //Signin
    public bool RequireConfirmedAccount { get; set; }
    public bool RequireConfirmedEmail { get; set; }
    public bool RequireConfirmedPhoneNumber { get; set; }
}