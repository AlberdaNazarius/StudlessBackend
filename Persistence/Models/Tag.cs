namespace StudlessBackend.Persistence.Models;

public class Tag
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public ICollection<QuestionTag>? QuestionTags { get; set; }
}