using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

using SH.Application.Interfaces;
using SH.Infrastructure.Extensions;

using System.Text;
using System.Text.Json;

namespace SH.Infrastructure.Services;

public class CookieHelper : ICookieHelper
{
    protected IHttpContextAccessor _httpContextAccessor { get; }
    protected bool IsNotDevelopment { get; }

    public CookieHelper(IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment webHostEnvironment)
    {
        _httpContextAccessor = httpContextAccessor;
        IsNotDevelopment = webHostEnvironment.IsStaging() || webHostEnvironment.IsProduction();
    }

    public T Get<T>(string key)
    {
        var content = _httpContextAccessor.HttpContext?.Request.Cookies[key];
        T data = default(T);

        if (IsNotDevelopment is true)
            content = Encoding.UTF8.GetString(Convert.FromBase64String(content));

        if (!string.IsNullOrWhiteSpace(content))
            data = JsonSerializer.Deserialize<T>(content);

        return data;
    }

    public string Get(string key)
    {
        if (IsNotDevelopment is true)
            return Encoding.UTF8.GetString(Convert.FromBase64String(_httpContextAccessor.HttpContext?.Request.Cookies[key]));
        else
            return _httpContextAccessor.HttpContext?.Request.Cookies[key];
    }

    public bool ContainsKey(string key)
    {
        return _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(key);
    }

    public bool ContainsValue(string key, string value)
    {
        if (IsNotDevelopment is true)
            return _httpContextAccessor.HttpContext.Request.Cookies[key] == Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        else
            return _httpContextAccessor.HttpContext.Request.Cookies[key] == value;
    }

    public void Set(string key, string value, CookieOptions options)
    {
        if (IsNotDevelopment is true)
            value = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, options);

        LogCookie($"Appended {key} to Cookie with Value {value}");
    }

    public void Set(string key, string value)
    {
        if (IsNotDevelopment is true)
            value = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value);

        LogCookie($"Appended {key} to Cookie with Value {value}");
    }

    public void Set<T>(string key, T value)
    {
        var serializedData = JsonSerializer.Serialize(value);

        if (IsNotDevelopment is true)
            serializedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedData));

        _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, serializedData);

        LogCookie($"Appended {key} to Cookie");
    }

    public void Delete(string key)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(key);

        LogCookie($"Cookie {key} Deleted by Successfully");
    }

    private void LogCookie(string message)
    {
        _httpContextAccessor.HttpContext?.RequestServices.CookieLogger(message);
    }
}