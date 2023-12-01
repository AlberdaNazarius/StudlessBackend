using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories;

public interface IQuestionRepository
{
    ICollection<Question> GetQuestions();
    Question? GetQuestion(long id);
    bool Save();
    bool AddQuestion(Question question, long tagId);
    bool UpdateQuestion(Question question);
    bool DeleteQuestion(Question questionToDelete);
}