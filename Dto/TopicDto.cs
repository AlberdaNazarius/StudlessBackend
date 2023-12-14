using System.ComponentModel.DataAnnotations;

namespace StudlessBackend.Dto;

public class TopicDto
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    public string ColorTheme { get; set; } = null!;
}