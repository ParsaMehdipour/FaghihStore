using Microsoft.AspNetCore.Http;

using SH.Application.Interfaces;

using System.Text;

namespace SH.Infrastructure.Services;

public class FileUploaderService : IFileUploaderService
{
    public string CreateFileName(string fileName, string prefixFileName, params string[] paths)
    {
        string uploadPath = Path.Combine("wwwroot", "Upload");

        uploadPath = $"{uploadPath}\\{Path.Combine(paths)}";

        CreateDirectory(uploadPath);

        var fileExtensionName = Path.GetExtension(fileName);

        fileName = GenerateFileName(prefixFileName);

        fileName += fileExtensionName;

        fileName = Path.Combine(uploadPath, fileName);

        return fileName;
    }

    public List<(IFormFile File, string NewFileName)> CreateFileName(List<IFormFile> files, string prefixFileName, params string[] paths)
    {
        List<(IFormFile File, string FileName)> fileNames = new(files.Count);

        files.ForEach(file =>
        {
            fileNames.Add((file, this.CreateFileName(file.FileName, prefixFileName, paths)));
        });

        return fileNames;
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

    public string GenerateFileName(string prefixName = null)
    {
        return prefixName += Path.GetRandomFileName();
    }

    public async Task SaveFile(IFormFile file, string fileName, FileMode fileMode, CancellationToken cancellationToken)
    {
        await using var fileStream = new FileStream(fileName, FileMode.Create);
        await file.CopyToAsync(fileStream, cancellationToken);
    }

    public async Task SaveFile(List<(IFormFile File, string NewFileName)> files, FileMode fileMode, CancellationToken cancellationToken)
    {
        files.ForEach(async file =>
        {
            await using var fileStream = new FileStream(file.NewFileName, fileMode);
            file.File.CopyTo(fileStream);
        });

        await Task.CompletedTask;
    }

    public string ReplaceFileNameAfterCreated(string fileName)
    {
        var stringBuilder = new StringBuilder(fileName);

        stringBuilder.Replace("\\", "/");
        stringBuilder.Replace("wwwroot", string.Empty);

        return stringBuilder.ToString();
    }

    public List<string> ReplaceFileNameAfterCreated(List<string> fileNames)
    {
        List<string> replacedFileNames = new(fileNames.Count);

        fileNames.ForEach(imageFullPath => replacedFileNames.Add(this.ReplaceFileNameAfterCreated(imageFullPath)));

        return replacedFileNames;
    }

    public string ReplaceFileNameBeforeDelete(string fileName)
    {
        var stringBuilder = new StringBuilder(fileName);

        stringBuilder.Replace("/", "\\");

        string currentPath = Directory.GetCurrentDirectory() + "\\wwwroot" + stringBuilder.ToString();

        if (File.Exists(currentPath) is false)
            return string.Empty;

        return currentPath;
    }

    public void DeleteFile(string fileName)
    {
        string filePath = ReplaceFileNameBeforeDelete(fileName);
        if (filePath != string.Empty)
            File.Delete(filePath);
    }

    [Obsolete("That is obsolete method and will be remove.")]
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

    [Obsolete("That is obsolete method and will be remove.")]
    public async Task Upload(List<IFormFile> files, CancellationToken cancellationToken, params string[] paths)
    {
        List<Task> tasks = new();

        foreach (var file in files)
            tasks.Add(this.Upload(file, cancellationToken, paths));

        await Task.WhenAll(tasks);
    }
}