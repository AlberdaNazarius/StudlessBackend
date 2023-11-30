using Microsoft.EntityFrameworkCore;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Question>? Questions { get; set; }
    public DbSet<Answer>? Answers { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}