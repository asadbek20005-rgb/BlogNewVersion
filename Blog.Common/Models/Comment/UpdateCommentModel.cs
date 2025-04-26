namespace Blog.Common.Models.Comment;

public class UpdateCommentModel
{
    public Guid? SenderId { get; set; }
    public Guid? RecieverId { get; set; }
    public string? Message { get; set; } = string.Empty;
}
