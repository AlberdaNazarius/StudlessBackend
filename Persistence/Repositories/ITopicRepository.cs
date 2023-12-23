using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories;

public interface ITopicRepository
{
    Task<ICollection<Topic>> GetTopics();
    Task<Topic?> GetTopic(long id);
    Task<bool> Save();
    Task<bool> AddTopic(Topic topic);
    Task<bool> AddQuestionToTopic(Topic topic, Question question);
    Task<bool> DeleteTopic(Topic topic);
}