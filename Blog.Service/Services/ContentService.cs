using Blog.Data.Contracts;
using Blog.Data.Entities;
using Blog.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Blog.Service.Services;

public class ContentService(Dependencies.ServiceDependencies dependencies) : IContentService
{
    private readonly IMinioService _minioService = dependencies.MinioService;
    private readonly IBaseRepository<Content> _contentRepsitory = dependencies.ContentRepository;
    public async Task<Stream> DownloadFileAsync(Guid userId, string fileName)
    {
        bool fileNameIsAppropriateToUser = await _contentRepsitory
            .GetAll()
            .AnyAsync(x => x.UserId == userId && x.FileUrl == fileName);
        if (fileNameIsAppropriateToUser)
        {
            var file = await _minioService.GetFileAsync(fileName);
            return file;

        }

        return Stream.Null;
    }

    public async Task<string> UploadFileAsync(Guid userId, IFormFile file)
    {
        var (fileName, contentType, size, data) = await SaveFileDetailsAsync(file);
        await _minioService.UploadFileAsync(fileName, data, size, contentType);
        data.Position = 0;
        var newContent = new Content
        {
            FileUrl = fileName,
            Name = file.Name,
            UserId = userId,
            ContentType = contentType
        };
        await _contentRepsitory.AddAsync(newContent);
        await _contentRepsitory.SaveChangesAsync();
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

    //private bool FileIsExist(string fileName)
    //{

    //}
}
