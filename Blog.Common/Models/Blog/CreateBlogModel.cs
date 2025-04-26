using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Blog.Common.Models.BaseModels;

namespace Blog.Common.Models.Blog;

public class CreateBlogModel : BaseCreateModel
{
    [Column("description")]
    [DataType(DataType.Text)]
    [Required]
    public string Description { get; set; } = string.Empty;
}
