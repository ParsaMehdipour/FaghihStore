using FluentResults;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

using SH.Application.Interfaces;
using SH.Application.Models;
using SH.Infrastructure.Consts;
using SH.Infrastructure.Extensions;

using System.Text;
using System.Text.Json;

namespace SH.Infrastructure.Services;

public class SmsService : ISmsService
{
    protected IHttpClientFactory _httpClientFactory { get; }
    protected ICookieHelper _cookieHelper { get; }
    protected bool IsNotDevelopment { get; }

    public SmsService(IHttpClientFactory httpClientFactory,
        ICookieHelper cookieHelper,
        IWebHostEnvironment webHostEnvironment)
    {
        _httpClientFactory = httpClientFactory;
        _cookieHelper = cookieHelper;
        IsNotDevelopment = webHostEnvironment.IsStaging() || webHostEnvironment.IsProduction();
    }

    /// <summary>
    /// this implementation to send activation code to PhoneNumber or Email when sms panel is broken or not any responses from server.
    /// </summary>
    /// <param name="dto">the user PhoneNumber and Email</param>
    /// <param name="cancellationToken"></param>
    public Task<Result> SendActivationCodeAsync(SmsServiceDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// this implementation is just to send activation code to PhoneNumber when must send SMS.
    /// </summary>
    /// <param name="phoneNumber">the user phone number.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result> SendActivationCodeAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var random = Random.Shared.ActivationCode(10000, 99999).ToString();

        string cookieValue = $"{phoneNumber}-{random}";
        SetActivationCodeCookie(cookieValue);

        if (IsNotDevelopment is true)
            await SendOTPCode(phoneNumber, random, cancellationToken);

        return Result.Ok();
    }

    private void SetActivationCodeCookie(string value)
    {
        if (!_cookieHelper.ContainsValue(IdentityConsts.CookieActivationCodeKey, value))
        {
            CookieOptions cookieOptions = new()
            {
                Expires = DateTime.Now.AddSeconds(500),
                SameSite = SameSiteMode.Lax,
                HttpOnly = true,
                Path = "/",
            };

            _cookieHelper.Set(IdentityConsts.CookieActivationCodeKey, value, cookieOptions);
        }
    }

    private async Task SendOTPCode(string phoneNumber, string code, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("SMS");

        client.BaseAddress = new("https://api.sms.ir");
        client.DefaultRequestHeaders.Add("x-api-key", "vUp8sLnYfNeCKP2WTMvSmBfc261utuAL1mhxZJi3uJ7SsG5qb75dCieneRBvFn4M");

        var postSms = new PostSms
        {
            Mobile = phoneNumber,
            TemplateId = "672189",
            Parameters = new(1)
        };
        postSms.Parameters.Add(new()
        {
            Name = "Code",
            Value = code
        });

        var json = JsonSerializer.Serialize(postSms);
        var content = new StringContent(json, Encoding.UTF8, mediaType: "application/json");
        await client.PostAsync("v1/send/verify", content, cancellationToken);
    }
}