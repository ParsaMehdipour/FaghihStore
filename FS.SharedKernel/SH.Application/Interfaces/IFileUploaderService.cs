using Microsoft.AspNetCore.Http;

namespace SH.Application.Interfaces;

public interface IFileUploaderService
{
    string CreateFileName(string fileName, string prefixFileName, params string[] paths);
    List<(IFormFile File, string NewFileName)> CreateFileName(List<IFormFile> files, string prefixFileName, params string[] paths);
    string GenerateFileName(string prefixName);
    string ReplaceFileNameAfterCreated(string fileName);
    List<string> ReplaceFileNameAfterCreated(List<string> fileNames);
    string ReplaceFileNameBeforeDelete(string fileName);
    Task SaveFile(IFormFile file, string fileName, FileMode fileMode, CancellationToken cancellationToken);
    Task SaveFile(List<(IFormFile File, string NewFileName)> files, FileMode fileMode, CancellationToken cancellationToken);
    void DeleteFile(string fileName);

    [Obsolete("That is obsolete method and will be remove.")]
    Task<string> Upload(IFormFile file, CancellationToken cancellationToken, params string[] paths);

    [Obsolete("That is obsolete method and will be remove.")]
    Task Upload(List<IFormFile> files, CancellationToken cancellationToken, params string[] paths);
}