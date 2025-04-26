using Blog.Common.Models.BaseModels;

namespace Blog.Common.Models.Post;

public class UpdatePostModel : BaseUpdateModel
{
    public string? Description { get; set; } = string.Empty;



    public int? BlogId { get; set; }



    public int? StatusId { get; set; }

}
