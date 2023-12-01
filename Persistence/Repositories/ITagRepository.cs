using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories;

public interface ITagRepository
{
    ICollection<Tag> GetTags();
    Tag? GetTag(long id);
    bool Save();
    bool AddTag(Tag tag);
    bool DeleteTag(Tag tagToDelete);
}