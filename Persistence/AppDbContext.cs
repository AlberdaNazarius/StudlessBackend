using Microsoft.EntityFrameworkCore;
using StudlessBackend.Persistence.Models;

namespace StudlessBackend.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Question>? Questions { get; set; }
    public DbSet<Answer>? Answers { get; set; }
    public DbSet<Tag>? Tags { get; set; }
    public DbSet<Topic>? Topics { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionTag>()
            .HasKey(qt => new { qt.QuestionId, qt.TagId });
        modelBuilder.Entity<QuestionTag>()
            .HasOne(q => q.Question)
            .WithMany(qt => qt.QuestionTags)
            .HasForeignKey(qt => qt.QuestionId);
        modelBuilder.Entity<QuestionTag>()
            .HasOne(q => q.Tag)
            .WithMany(qt => qt.QuestionTags)
            .HasForeignKey(qt => qt.TagId);

    }
}