using Identity.Policies;

namespace Identity.Extensions;

public static class HttpClientExtension
{
    [Obsolete("this method not be use.")]
    public static void AddIdentityHttpClient(this IServiceCollection services)
    {
        services.AddSingleton<ClientIdentityPolicy>();

        services.AddHttpClient("Auth", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7204/api/Auth/");
        }).AddPolicyHandler(new ClientIdentityPolicy().LinearHttpRetry);
    }
}