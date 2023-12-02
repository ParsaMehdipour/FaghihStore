using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace SH.Infrastructure.Services;

public class CookieManager : ICookieManager
{
    protected ICookieManager ConcreteManager { get; }
    protected IWebHostEnvironment Environment { get; }

    public CookieManager(IWebHostEnvironment environment)
    {
        ConcreteManager = new ChunkingCookieManager();
        Environment = environment;
    }

    public void AppendResponseCookie(HttpContext context, string key, string value, CookieOptions options)
    {
        options.Domain = GetCookieHostUrl(context);

        ConcreteManager.AppendResponseCookie(context, key, value, options);
    }

    public void DeleteCookie(HttpContext context, string key, CookieOptions options)
    {
        options.Domain = GetCookieHostUrl(context);

        ConcreteManager.DeleteCookie(context, key, options);
    }

    public string GetRequestCookie(HttpContext context, string key)
    {
        return ConcreteManager.GetRequestCookie(context, key);
    }

    public string GetCookieHostUrl(HttpContext context)
    {
        string host;
        if (Environment.IsDevelopment())
            host = "localhost";
        else
            host = string.Join('.', context.Request.Host.Value.Split('.').Skip(1));

        return host;
    }
}