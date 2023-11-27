using Microsoft.EntityFrameworkCore;

namespace StudlessBackend.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}