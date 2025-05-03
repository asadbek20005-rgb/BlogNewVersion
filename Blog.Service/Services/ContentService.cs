using Blog.Service.Contracts;
using Microsoft.AspNetCore.Http;

namespace Blog.Service.Services;

public class ContentService(Dependencies.ServiceDependencies dependencies) : IContentService
{
    private readonly IMinioService _minioService = dependencies.MinioService;
    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var (fileName, contentType, size, data) = await SaveFileDetailsAsync(file);
        await _minioService.UploadFileAsync(fileName, data, size, contentType);
        data.Position = 0;
        return fileName;
    }


    private async Task<(string FileName, string ContentType, long Size, MemoryStream Data)> SaveFileDetailsAsync(IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString();
        string contentType = file.ContentType;
        long size = file.Length;

        var data = new MemoryStream();
        await file.CopyToAsync(data);

        return (fileName, contentType, size, data);
    }
}
