using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.Otp;

public class OtpModel
{
    [StringLength(15)]
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public int Code { get; set; }
}
