namespace Blog.Common.Models.PostStatus;

public class PostStatusModel
{
    public bool IsPublished { get; set; } = false;
    public bool IsLiked { get; set; } = false;
    public int ViewsCount { get; set; } = 0;
    public bool IsSaved { get; set; } = false;
}
