using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.User;

public class RegisterModel
{
    [StringLength(50)]
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [StringLength(50)]
    [Required]
    public string LastName { get; set; } = string.Empty;
    [StringLength(15)]
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;


    [Required]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [DataType(DataType.Text)]
    public string? Bio { get; set; }



    [DataType(DataType.Date)]
    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public int GenderId { get; set; }

}
