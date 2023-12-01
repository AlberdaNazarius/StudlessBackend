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

    public Question GetQuestion(long id)
    {
        var retrievedQuestion = _context.Questions!.FirstOrDefault(q => q!.Id == id);
        if (retrievedQuestion == null)
            throw new InvalidOperationException();
        return retrievedQuestion;
    }

    public bool AddQuestion(Question question, long tagId)
    {
        _context.Add(question);
        return _context.SaveChanges() > 0;
    }
}