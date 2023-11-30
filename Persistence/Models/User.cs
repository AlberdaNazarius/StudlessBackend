using StudlessBackend.Enums;

namespace StudlessBackend.Persistence.Models;

public class User
{
    public long Id { get; set; }
    public string? Name { get; set; }
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
    public ICollection<Answer>? Answers { get; set; }
    public ICollection<Question>? Questions { get; set; }
}