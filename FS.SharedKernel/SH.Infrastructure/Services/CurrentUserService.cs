using Microsoft.AspNetCore.Http;
using SH.Application.Interfaces;
using System.Security.Claims;

namespace SH.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    protected IHttpContextAccessor _httpContextAccessor { get; }
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId => Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    //public Guid? UserId => null;

    public string UserIpAddress => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress.ToString();
}