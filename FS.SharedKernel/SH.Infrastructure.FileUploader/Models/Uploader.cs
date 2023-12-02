using Microsoft.AspNetCore.Http;

using SH.Infrastructure.FileUploader.Enums;

namespace SH.Infrastructure.FileUploader.Models;
public class Uploader
{
    public Uploader(string inputId)
    {
        UploadOperation = UploadOperation.Upload;

        InputId = $"{inputId}_{nameof(File)}";
    }

    public Uploader(string url, string name, string inputId)
    {
        Name = string.IsNullOrWhiteSpace(name) ? url.Split("/").Last() : name;

        Url = url;
        UploadOperation = UploadOperation.Read;

        InputId = $"{inputId}_{nameof(File)}";
    }

    public IFormFile File { get; set; }
    public string Url { get; private set; }
    public string Name { get; private set; }

    /// <summary>
    /// the input element id attribute in html.
    /// </summary>
    public string InputId { get; private set; }
    public UploadOperation UploadOperation { get; private set; }

    public void SetUrl(string url)
    {
        Url = url;
    }
}