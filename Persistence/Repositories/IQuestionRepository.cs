using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories;

public interface IQuestionRepository
{
    ICollection<Question> GetQuestions();
    Question GetQuestion(long id);
    bool AddQuestion(Question question, long tagId);
}