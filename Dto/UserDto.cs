using StudlessBackend.Enums;

namespace StudlessBackend.Dto;

public class UserDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Email {get; set;}
    public string? Password {get; set;}
    public string? Role { get; set; }
    public string? ImagePath { get; set; }
    public string? BannerPath { get; set; }
    public int Rating { get; set; }
    public int MessagesCount { get; set; }
    public int SolutionCount { get; set; }
    public int FeaturedContentCount { get; set; }
    public int ReactionScoreCount { get; set; }
    public int Points { get; set; }
    public DateTime JoinDate { get; set; }
    public UserStatus OnlineStatus { get; set; }
    public int[]? AnswersId {get; set;}
    public int[]? QuestionsId {get; set;}
}