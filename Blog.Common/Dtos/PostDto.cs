namespace Blog.Common.Dtos;

public class PostDto : BaseDto
{
    public string Description { get; set; } = string.Empty;
    public int BlogId { get; set; }
    public int StatusId { get; set; }

}