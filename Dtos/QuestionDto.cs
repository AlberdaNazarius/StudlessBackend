using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Dtos;

public class QuestionDto
{
    public long Id { get; set; }
    public string? QuestionName { get; set; }
    public string? Description { get; set; }
    public int Votes { get; set; }
    public int Views { get; set; }
    public DateTime AskedDate { get; set; }
    public User? Author { get; set; }
    public ICollection<Answer>? Answers { get; set; }
}