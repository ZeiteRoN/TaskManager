using Microsoft.EntityFrameworkCore;
using TaskManager.Configuration;
using TaskManager.Models;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository: ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<TaskEntity?> GetTaskByIdAsync(Guid taskId)
    {
        return await _context.Tasks.
            Include(t => t.UserEntity)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public IQueryable<TaskEntity?> GetTaskByUserId(Guid userId)
    {
        return  _context.Tasks.Where(t => t != null && t.UserId == userId);
    }

    public async Task AddTaskAsync(TaskEntity task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskEntity task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(TaskEntity task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}