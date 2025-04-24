namespace Blog.Common.Dtos;

public class BlogDto : BaseDto
{
    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; }

}
