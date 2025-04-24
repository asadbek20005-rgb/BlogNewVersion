using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities;
[Table("posts_statuses")]
public class PostStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("is_published")]
    public bool IsPublished { get; set; } = false;

    [Column("is_liked")]
    public bool IsLiked { get; set; } = false;

    [Column("views_count")]
    public int ViewsCount { get; set; } = 0;

    [Column("is_saved")]
    public bool IsSaved { get; set; } = false;

    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [Column("updated_date")]
    public DateTime UpdatedDate { get; set; }
}
