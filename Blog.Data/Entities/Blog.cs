using Blog.Data.Entities.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities;
[Table("blogs")]
public class Blog : BaseEntity
{
    [Column("description")]
    [DataType(DataType.Text)]
    [Required]
    public string Description { get; set; } = string.Empty;

    [Column("user_id")]
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
