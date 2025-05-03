using Microsoft.AspNetCore.Http;

namespace Blog.Service.Contracts;

public interface IContentService
{
    Task<string> UploadFileAsync(IFormFile file);


}
