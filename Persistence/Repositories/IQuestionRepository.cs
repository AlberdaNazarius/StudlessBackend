using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories;

public interface IQuestionRepository
{
    ICollection<Question> GetQuestions();
    Question? GetQuestion(long id);
    bool Save();
    bool AddTag(long questionId, long tagId);
    bool AddQuestion(Question question);
    bool UpdateQuestion(Question question);
    bool DeleteQuestion(Question questionToDelete);
}