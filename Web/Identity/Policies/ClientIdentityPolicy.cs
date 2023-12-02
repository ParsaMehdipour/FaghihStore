using Polly;
using Polly.Retry;

using System.Net;

namespace Identity.Policies;

[Obsolete("this method not be use.")]
public class ClientIdentityPolicy
{
    public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }

    public ClientIdentityPolicy()
    {
        HttpStatusCode[] httpStatusCodesWorthRetrying = {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout // 504
        };

        LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(response => httpStatusCodesWorthRetrying.Contains(response.StatusCode))
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(3));
    }
}