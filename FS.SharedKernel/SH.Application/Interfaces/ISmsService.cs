using FluentResults;

using SH.Application.Models;

namespace SH.Application.Interfaces;

public interface ISmsService
{
    Task<Result> SendActivationCodeAsync(SmsServiceDto dto, CancellationToken cancellationToken);
    Task<Result> SendActivationCodeAsync(string phoneNumber, CancellationToken cancellationToken);
}