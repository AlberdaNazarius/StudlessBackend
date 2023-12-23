namespace StudlessBackend.Persistence.Models;

public class Topic
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string ColorTheme { get; set; } = null!;
    public ICollection<Question>? Questions { get; set; }
}