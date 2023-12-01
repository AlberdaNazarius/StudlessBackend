using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Dto;

public class QuestionDto
{
    public long Id { get; set; }
    public string? QuestionName { get; set; }
    public string? Description { get; set; }
    public int Votes { get; set; }
    public int Views { get; set; }
    public DateTime AskedDate { get; set; }
    public UserDto? Author { get; set; }
    public ICollection<AnswerDto>? Answers { get; set; }
    public ICollection<TagDto>? Tags { get; set; }
}