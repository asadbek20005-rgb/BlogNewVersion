using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.User;

public class LoginModel
{
    [StringLength(15)]
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
