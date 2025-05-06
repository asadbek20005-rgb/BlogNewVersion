using Microsoft.AspNetCore.Http;

namespace Blog.Service.Contracts;

public interface IContentService
{
    Task<string> UploadFileAsync(Guid userId,IFormFile file);
    Task<Stream> DownloadFileAsync(Guid userId,string fileName);

}
