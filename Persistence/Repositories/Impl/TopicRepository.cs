using Microsoft.EntityFrameworkCore;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories.Impl;

public class TopicRepository : ITopicRepository
{
    private readonly AppDbContext _context;

    public TopicRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Topic>> GetTopics()
    {
        return await _context.Topics!.ToListAsync();
    }

    public async Task<Topic?> GetTopic(long id)
    {
        return await _context.Topics!.Include(t => t.Questions).FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> Save()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddTopic(Topic topic)
    {
        await _context.AddAsync(topic);
        return await Save();
    }

    public async Task<bool> AddQuestionToTopic(Topic topic, Question question)
    {
        topic.Questions!.Add(question);
        _context.Update(question);
        return await Save();
    }

    public async Task<bool> DeleteTopic(Topic topic)
    {
        _context.Remove(topic);
        return await Save();
    }
}