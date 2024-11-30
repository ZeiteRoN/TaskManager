using Microsoft.EntityFrameworkCore;
using TaskManager.Configuration;
using TaskManager.Models;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<TaskEntity?> GetTaskByIdAsync(Guid taskId)
    {
        return await context.Tasks.
            Include(t => t.UserEntity)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public IQueryable<TaskEntity?> GetTaskByUserId(Guid userId)
    {
        return  context.Tasks.Where(t => t != null && t.UserId == userId);
    }

    public async Task AddTaskAsync(TaskEntity task)
    {
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskEntity task)
    {
        context.Tasks.Update(task);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(TaskEntity task)
    {
        context.Tasks.Remove(task);
        await context.SaveChangesAsync();
    }
}