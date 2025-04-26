using Blog.Common.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.Post;

public class CreatePostModel : BaseCreateModel
{
    [DataType(DataType.Text)]
    [Required]
    public string Description { get; set; } = string.Empty;


    [Required]
    public int BlogId { get; set; }


    [Required]
    public int StatusId { get; set; }

}
