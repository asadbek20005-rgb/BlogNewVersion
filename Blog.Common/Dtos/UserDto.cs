namespace Blog.Common.Dtos;

public class UserDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;


    public string PasswordHash { get; set; } = string.Empty;


    public string? Bio { get; set; }


    public DateOnly DateOfBirth { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; }

}
