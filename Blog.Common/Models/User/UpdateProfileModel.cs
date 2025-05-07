using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.User;

public class UpdateProfileModel
{
    [StringLength(50)]
    public string? FirstName { get; set; }
    [StringLength(50)]
    public string? LastName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Bio { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public int? GenderId { get; set; }

}
