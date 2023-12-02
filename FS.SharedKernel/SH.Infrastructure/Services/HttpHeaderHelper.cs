using Microsoft.AspNetCore.Http;
using SH.Application.Interfaces;
using SH.Infrastructure.Extensions;
using System.Text.Json;

namespace SH.Infrastructure.Services;

public class HttpHeaderHelper : IHttpHeaderHelper
{
    protected IHttpContextAccessor _httpContextAccessor { get; }

    public HttpHeaderHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public T Get<T>(string key)
    {
        var content = _httpContextAccessor.HttpContext?.Request.Headers[key];
        T data = default(T);

        if (!string.IsNullOrWhiteSpace(content))
            data = JsonSerializer.Deserialize<T>(content);

        return data;
    }

    public string Get(string key)
    {
        return _httpContextAccessor.HttpContext?.Request.Headers[key];
    }

    public bool ContainsKey(string key)
    {
        return _httpContextAccessor.HttpContext.Request.Headers.ContainsKey(key);
    }

    public bool ContainsValue(string key, string value)
    {
        return _httpContextAccessor.HttpContext.Request.Headers[key] == value;
    }

    public void Set(string key, string value)
    {
        _httpContextAccessor.HttpContext?.Request.Headers.Add(key, value);

        LogCookie($"Appended {key} to Header with Value {value}");
    }

    public void Set<T>(string key, T value)
    {
        var serializedData = JsonSerializer.Serialize(value);

        _httpContextAccessor.HttpContext?.Request.Headers.Add(key, serializedData);

        LogCookie($"Appended {key} to Header");
    }

    public void Delete(string key)
    {
        _httpContextAccessor.HttpContext?.Request.Headers.Remove(key);

        LogCookie($"Header {key} Deleted by Successfully");
    }

    private void LogCookie(string message)
    {
        _httpContextAccessor.HttpContext?.RequestServices.CookieLogger(message);
    }
}
