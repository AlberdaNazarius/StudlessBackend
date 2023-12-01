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

    public Question? GetQuestion(long id)
    {
        return _context.Questions!.FirstOrDefault(q => q.Id == id);
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }

    public bool AddQuestion(Question question, long tagId)
    {
        _context.Add(question);
        return Save();
    }

    public bool UpdateQuestion(Question question)
    {
        _context.Update(question);
        return Save();
    }

    public bool DeleteQuestion(Question questionToDelete)
    {
        _context.Remove(questionToDelete);
        return Save();
    }
}