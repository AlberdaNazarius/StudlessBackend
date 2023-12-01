using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories.Impl;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public ICollection<Tag> GetTags()
    {
        return _context.Tags!.ToList();
    }

    public Tag? GetTag(long id)
    {
        return _context.Tags!.FirstOrDefault(e => e.Id == id);
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }

    public bool AddTag(Tag tag)
    {
        _context.Add(tag);
        return Save();
    }

    public bool DeleteTag(Tag tagToDelete)
    {
        _context.Remove(tagToDelete);
        return Save();
    }
}