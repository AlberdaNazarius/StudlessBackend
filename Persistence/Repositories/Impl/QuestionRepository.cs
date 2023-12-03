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

    public async Task<ICollection<Question>> GetQuestions()
    {
        return await _context.Questions!
            .Include(q => q.Author)
            .Include(q => q.QuestionTags)
            .Include(q => q.Answers)
            .OrderBy(q => q.Id)
            .ToListAsync();
    }

    public async Task<Question?> GetQuestion(long id)
    {
        var question = await _context.Questions!
            .Include(q => q.Author)
            .Include(q => q.QuestionTags)
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
            return null;

        foreach (var questionTag in question.QuestionTags!)
        {
            var tag = await _context.Tags!.FirstOrDefaultAsync(t => t.Id == questionTag.TagId);
            questionTag.Tag = tag;
        }

        foreach (var answer in question.Answers!)
        {
            var answerWithAuthor = await _context.Answers!
                .Include(a => a.Author)
                .FirstOrDefaultAsync(a => a.Id == answer.Id);
            answer.Author = answerWithAuthor!.Author;
        }
        
        return question;
    }

    public async Task<bool> Save()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> AddTag(long questionId, long tagId)
    {
        var question = await _context.Questions!.FirstOrDefaultAsync(q => q.Id == questionId);
        var tag = await _context.Tags!.FirstOrDefaultAsync(t => t.Id == tagId);
        
        var questionTag = new QuestionTag()
        {
            Question = question,
            Tag = tag
        };
        
        await _context.AddAsync(questionTag);
        return await Save();
    }

    public async Task<bool> AddQuestion(Question question)
    {
        await _context.AddAsync(question);
        return await Save();
    }

    public async Task<bool> UpdateQuestion(Question question)
    {
        _context.Update(question);
        return await Save();
    }

    public async Task<bool> DeleteQuestion(Question questionToDelete)
    {
        _context.Remove(questionToDelete);
        return await Save();
    }
}