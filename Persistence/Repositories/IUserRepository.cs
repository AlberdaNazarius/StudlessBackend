using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories;

public interface IUserRepository
{
    Task<User?> GetUser(long id);
    Task<bool> Save();
    Task<bool> AddUser(User user);
    public User FindByEmail(string email);
    User FindById(long id);
    Task<ICollection<User>> GetUsers();
    Task<bool> DeleteUser(long id);

}