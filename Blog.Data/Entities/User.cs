using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities;
[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("first_name")]
    [StringLength(50)]
    [Required]
    public string FirstName { get; set; } = string.Empty;


    [Column("last_name")]
    [StringLength(50)]
    [Required]
    public string LastName { get; set; } = string.Empty;

    [Column("phone_number")]
    [StringLength(15)]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;


    [Column("password_hash")]
    [StringLength(255)]
    [Required]
    public string PasswordHash { get; set; } = string.Empty;


    [Column("bio")]
    [DataType(DataType.Text)]
    public string? Bio { get; set; }


    [Column("date_of_birth")]
    [DataType(DataType.Date)]
    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Column("created_date")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [Column("updated_date")]
    [DataType(DataType.DateTime)]
    public DateTime UpdatedDate { get; set; }


    public ICollection<Blog>? Blogs { get; set; }
}
