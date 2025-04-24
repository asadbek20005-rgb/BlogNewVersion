using Blog.Data.Entities.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities;
[Table("posts")]
public class Post : BaseEntity
{
    [Column("description")]
    [DataType(DataType.Text)]
    [Required]
    public string Description { get; set; } = string.Empty;


    [Column("blog_id")]
    [Required]
    public int BlogId { get; set; }

    [ForeignKey(nameof(BlogId))]
    public Blog? Blog { get; set; }

    [Column("status_id")]
    [Required]
    public int StatusId { get; set; }

    [ForeignKey(nameof(StatusId))]
    public PostStatus? Status { get; set; }

    public ICollection<Comment>? Comments { get; set; } 
}
