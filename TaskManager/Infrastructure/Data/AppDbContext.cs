using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity?> Users { get; set; }
    public DbSet<TaskEntity?> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.UserEntity)
            .HasForeignKey(t => t.UserId);
        
        modelBuilder.Entity<TaskEntity>()
            .HasKey(t => t.Id);

        base.OnModelCreating(modelBuilder);
    }
}