using Blog.Common.Models.BaseModels;

namespace Blog.Common.Models.Blog;

public class UpdateBlogModel : BaseUpdateModel
{

    public string? Description { get; set; } = string.Empty;
}
