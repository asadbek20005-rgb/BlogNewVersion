using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.Comment;

public class CreateCommentModel
{
    [Required]
    public Guid SenderId { get; set; }



    [Required]
    public Guid RecieverId { get; set; }


    [DataType(DataType.Text)]
    [Required]
    public string Message { get; set; } = string.Empty;


}
