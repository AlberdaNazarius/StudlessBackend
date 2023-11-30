using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories.Impl;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _context;
    
    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public ICollection<Question> GetQuestions()
    {
        return _context.Questions!.OrderBy(q => q.Id).ToList();
    }
}