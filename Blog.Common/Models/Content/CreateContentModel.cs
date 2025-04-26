using Microsoft.AspNetCore.Http;

namespace Blog.Common.Models.Content;

public class CreateContentModel
{
    public IFormFile? File { get; set; } 
}
