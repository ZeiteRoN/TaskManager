using Microsoft.EntityFrameworkCore;
using TaskManager.API.DTOs;
using TaskManager.Configuration;
using TaskManager.Models;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(Guid userId, TaskFilterDto filter)
    {
        var query = _context.Tasks.AsQueryable().Where(t => t.UserId == userId);

        if (filter.Status.HasValue)
            query = query.Where(t => t.Status == filter.Status);

        if (filter.DueDate.HasValue)
            query = query.Where(t => t.DueDate == filter.DueDate);

        if (filter.Priority.HasValue)
            query = query.Where(t => t.Priority == filter.Priority);

        return await query.OrderBy(t => t.DueDate).ToListAsync();
    }

    public async Task<TaskEntity?> GetTaskByIdAsync(Guid userId, Guid taskId)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
    }

    public async Task AddTaskAsync(TaskEntity task)
    {
        await _context.Tasks.AddAsync(task);
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
