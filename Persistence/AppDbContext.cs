using Microsoft.EntityFrameworkCore;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Question>? Questions { get; set; }
    public DbSet<Answer>? Answers { get; set; }
    public DbSet<Tag>? Tags { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionTag>()
            .HasKey(qt => new { qt.QuestionId, qt.TagId });

        modelBuilder.Entity<Question>()
            .HasMany(q => q.QuestionTags)
            .WithOne(qt => qt.Question)
            .HasForeignKey(qt => qt.QuestionId);

        modelBuilder.Entity<Tag>()
            .HasMany(t => t.QuestionTags)
            .WithOne(qt => qt.Tag)
            .HasForeignKey(qt => qt.TagId);
    }
}