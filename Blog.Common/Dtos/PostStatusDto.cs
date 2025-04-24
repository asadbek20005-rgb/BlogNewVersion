using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Common.Dtos;

public class PostStatusDto
{
    public int Id { get; set; }

    public bool IsPublished { get; set; } = false;

    public bool IsLiked { get; set; } = false;

    public int ViewsCount { get; set; } = 0;

    public bool IsSaved { get; set; } = false;

    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; }
}

    