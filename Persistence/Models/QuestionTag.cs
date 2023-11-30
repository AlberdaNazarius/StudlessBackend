namespace StudlessBackend.Persistence.Models;

public class QuestionTag
{
    public long QuestionId { get; set; }
    public Question? Question { get; set; }

    public long TagId { get; set; }
    public Tag? Tag { get; set; }
}