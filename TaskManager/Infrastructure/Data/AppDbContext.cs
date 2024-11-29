using Microsoft.EntityFrameworkCore;
using TaskManager.Models;
using Task = TaskManager.Models.Task;

namespace TaskManager.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
        
        modelBuilder.Entity<Task>()
            .HasKey(t => t.Id);

        base.OnModelCreating(modelBuilder);
    }
}