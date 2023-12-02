namespace SH.Application.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string UserIpAddress { get; }
}