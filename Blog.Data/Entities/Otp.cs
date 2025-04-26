using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities;
[Table("otps")]
public class Otp
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("phone_number")]
    [StringLength(15)]
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Column("code")]
    [Required]
    public int Code { get; set; }


    [Column("created_date")]        
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;    


    [Column("updated_date")]
    [DataType(DataType.DateTime)]
    public DateTime UpdatedDate { get; set; }

    [Column("is_expired")]
    public required bool IsExpired { get; set; } 

}
