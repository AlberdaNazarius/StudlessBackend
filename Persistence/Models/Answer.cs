namespace StudlessBackend.Persistence.Models;

public class Answer
{
    public long Id { get; set; }
    public int VotesCount { get; set; }
    public string? Description { get; set; }
    public User? Author { get; set; }
    public DateTime AnsweredDate { get; set; }
}