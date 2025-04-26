using System.ComponentModel.DataAnnotations;

namespace Blog.Common.Models.BaseModels;

public class BaseCreateModel
{
    [MaxLength(50)]
    [Required]
    public string Name { get; set; } = string.Empty;
}
