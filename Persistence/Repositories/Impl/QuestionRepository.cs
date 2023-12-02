using Microsoft.EntityFrameworkCore;
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
        var question = _context.Questions!
            .Include(q => q.Author)
            .Include(q => q.QuestionTags)
            .Include(q => q.Answers)
            .FirstOrDefault(q => q.Id == id);

        foreach (var questionTag in question!.QuestionTags!)
        {
            var tag = _context.Tags!.FirstOrDefault(e => e.Id == questionTag.TagId);
            questionTag.Tag = tag;
        }

        foreach (var answer in question.Answers!)
        {
            var answerWithAuthor = _context.Answers!
                .Include(e => e.Author)
                .FirstOrDefault(e => e.Id == answer.Id);
            answer.Author = answerWithAuthor!.Author;
        }
        
        return question;
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }
    
    public bool AddTag(long questionId, long tagId)
    {
        var question = _context.Questions!.FirstOrDefault(e => e.Id == questionId);
        var tag = _context.Tags!.FirstOrDefault(e => e.Id == tagId);
        
        var questionTag = new QuestionTag()
        {
            Question = question,
            Tag = tag
        };
        
        _context.Add(questionTag);
        return Save();
    }

    public bool AddQuestion(Question question)
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