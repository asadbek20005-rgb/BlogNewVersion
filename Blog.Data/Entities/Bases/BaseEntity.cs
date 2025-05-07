using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities.Bases;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(50)]
    [Required]
    public string Name { get; set; } = string.Empty;


    [Column("created_date")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [Column("updated_date")]
    [DataType(DataType.DateTime)]
    public DateTime UpdatedDate { get; set; }
}
