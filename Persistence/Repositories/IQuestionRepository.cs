using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories;

public interface IQuestionRepository
{
    Task<ICollection<Question>> GetQuestions();
    Task<Question?> GetQuestion(long id);
    Task<bool> Save();
    Task<bool> AddTag(long questionId, long tagId);
    Task<bool> AddQuestion(Question question);
    Task<bool> UpdateQuestion(Question question);
    Task<bool> DeleteQuestion(Question questionToDelete);
}