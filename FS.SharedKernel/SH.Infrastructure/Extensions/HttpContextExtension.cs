
using Microsoft.AspNetCore.Http;

namespace SH.Infrastructure.Extensions;

public static class HttpContextExtension
{
    public static string GetDomainUrl(this HttpRequest request)
    {
        return $"{request.Scheme}://{request.Host}";
    }

    /// <summary>
    /// this method will return Url that contains Scheme,host,path and query strings.
    /// for example: https://localhost:xxxx/path?queryString=xxxxx
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string GetFullUrl(this HttpRequest request)
    {
        return $"{request.GetDomainUrl()}{GetPath(request)}{GetQueryStrings(request)}";
    }

    public static string GetPath(this HttpRequest context)
    {
        return context.Path.ToString().ToLower();
    }

    public static string GetQueryStrings(this HttpRequest request)
    {
        return request.QueryString.ToString().ToLower();
    }
}