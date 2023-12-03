using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories;

public interface ITagRepository
{
    Task<ICollection<Tag>> GetTags();
    Task<Tag?> GetTag(long id);
    Task<bool> Save();
    Task<bool> AddTag(Tag tag);
    Task<bool> DeleteTag(Tag tagToDelete);
}