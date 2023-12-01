namespace StudlessBackend.Dto;

public class AnswerDto
{
    public long Id { get; set; }
    public int VotesCount { get; set; }
    public string? Description { get; set; }
    public UserDto? Author { get; set; }
    public DateTime AnsweredDate { get; set; }
}