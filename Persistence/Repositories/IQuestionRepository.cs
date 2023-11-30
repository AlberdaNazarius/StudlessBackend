using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories;

public interface IQuestionRepository
{
    ICollection<Question> GetQuestions();
}