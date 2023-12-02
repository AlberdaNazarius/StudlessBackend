using Microsoft.EntityFrameworkCore;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories.Impl;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Tag>> GetTags()
    {
        return await _context.Tags!.ToListAsync();
    }

    public async Task<Tag?> GetTag(long id)
    {
        return await _context.Tags!.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> Save()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddTag(Tag tag)
    {
        await _context.AddAsync(tag);
        return await Save();
    }

    public async Task<bool> DeleteTag(Tag tagToDelete)
    {
        _context.Remove(tagToDelete);
        return await Save();
    }
}