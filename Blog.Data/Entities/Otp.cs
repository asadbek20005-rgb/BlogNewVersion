using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entities;

public class Otp
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("phone_number")]
    [StringLength(15)]
    [Required]
    [Phone]
    public string Phone { get; set; }

    [Column("code")]
    [Required]
    public int Code { get; set; }
}
