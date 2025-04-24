using Blog.Data.Entities.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Blog.Data.Entities;
[Table("contents")]
public class Content : BaseEntity
{
    [Column("file_url")]
    [DataType(DataType.Text)]
    [Required]
    public string FileUrl { get; set; } = string.Empty;

    [Column("content_type")]
    [StringLength(50)]
    [Required]
    public string ContentType { get; set; } = string.Empty;



    [Column("post_id")]
    public int? PostId { get; set; }

    [ForeignKey(nameof(PostId))]
    public Post? Post { get; set; }


    [Column("blog_id")]
    public int? BlogId { get; set; }

    [ForeignKey(nameof(BlogId))]
    public Blog? Blog { get; set; }


    [Column("user_id")]
    public Guid? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

}
