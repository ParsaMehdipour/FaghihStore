using Microsoft.AspNetCore.Http;

using SH.Infrastructure.FileUploader.Interfaces;

namespace SH.Infrastructure.FileUploader.Services;

public class FileUploaderService : IFileUploaderService
{
    public async Task<string> Upload(IFormFile file, CancellationToken cancellationToken, params string[] paths)
    {
        string uploadPath = Path.Combine("wwwroot", "Upload");

        uploadPath = $"{uploadPath}\\{Path.Combine(paths)}";

        CreateDirectory(uploadPath);

        string fileName = Path.GetRandomFileName();

        fileName += Path.GetExtension(file.FileName);

        fileName = Path.Combine(uploadPath, fileName);

        await using var fileStream = new FileStream(fileName, FileMode.Create);
        await file.CopyToAsync(fileStream, cancellationToken);

        fileName = fileName.Replace("\\", "/");
        fileName = fileName.Replace("wwwroot", string.Empty);

        return fileName;
    }

    public async Task Upload(IFormFileCollection files, CancellationToken cancellationToken, params string[] paths)
    {
        List<Task> tasks = new();

        foreach (var file in files)
            tasks.Add(this.Upload(file, cancellationToken, paths));

        await Task.WhenAll(tasks);
    }

    private void CreateDirectory(params string[] paths)
    {
        foreach (var path in paths)
        {
            Console.WriteLine(path);
            if (Directory.Exists(path) is false)
                Directory.CreateDirectory(path);
        }
    }
}