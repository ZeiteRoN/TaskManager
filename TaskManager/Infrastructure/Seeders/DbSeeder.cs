using TaskManager.Configuration;
using TaskManager.Models;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.Infrastructure.Seeders;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new UserEntity
                {
                    Username = "testuser",
                    Email = "testuser@example.com",
                    PasswordHash = "hashedpassword",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
            context.SaveChanges();
        }
        
        if (!context.Tasks.Any())
        {
            var userId = context.Users.First().Id;
            context.Tasks.AddRange(
                new TaskEntity
                {
                    Title = "Test Task 1",
                    Description = "This is a test task.",
                    DueDate = DateTime.UtcNow.AddDays(7),
                    Status = TaskStatus.Pending,
                    Priority = TaskPriority.Medium,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new TaskEntity
                {
                    Title = "Test Task 2",
                    Description = "Another test task.",
                    DueDate = DateTime.UtcNow.AddDays(10),
                    Status = TaskStatus.Completed,
                    Priority = TaskPriority.High,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
            context.SaveChanges();
        }
    }

}
