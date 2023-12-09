using Microsoft.EntityFrameworkCore;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence.Repositories.Impl;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<User?> GetUser(long id)
    {
        var user = await _context.Users!
            .Include(u => u.Answers)
            .Include(u => u.Questions)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return null;
        
        return user;
    }

    public async Task<bool> Save()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    public User FindByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }
    public User FindById(long id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
    public async Task<bool> DeleteUser(long id){
            _context.Users.Remove(FindById(id));
            return await Save();
    }

    public async Task<bool> AddUser(User user)
    {
        await _context.AddAsync(user);
        return await Save();
    }
     public async Task<ICollection<User>> GetUsers()
    {
        return await _context.Users!
            .Include(u => u.Questions)
            .Include(u => u.Answers)
            .OrderBy(u => u.Id)
            .ToListAsync();
    }
}