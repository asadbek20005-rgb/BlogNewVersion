using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities;
[Table("Comments")]
public class Comment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("sender_id")]
    [Required]
    public Guid SenderId { get; set; }
    [ForeignKey(nameof(SenderId))]
    public User? Sender { get; set; }

    [Column("reciever_id")]
    [Required]
    public Guid RecieverId { get; set; }

    [ForeignKey(nameof(RecieverId))]
    public User? Reciever { get; set; }


    [Column("message")]
    [DataType(DataType.Text)]
    [Required]
    public string Message { get; set; } = string.Empty;


    [Column("created_date")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [Column("updated_date")]
    [DataType(DataType.DateTime)]
    public DateTime UpdatedDate { get; set; }


    [Column("post_id")]
    [Required]
    public int PostId { get; set; }
    [ForeignKey(nameof(PostId))]
    public Post? Post { get; set; }
}

