using Microsoft.AspNetCore.Http;

namespace SH.Infrastructure.FileUploader.Interfaces;

public interface IFileUploaderService
{
    Task<string> Upload(IFormFile file, CancellationToken cancellationToken, params string[] paths);
    Task Upload(IFormFileCollection files, CancellationToken cancellationToken, params string[] paths);
}