using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Models;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.Application.Services;

public class TaskService: ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    public async Task<TaskEntity> CreateTaskForUserAsync(Guid userId, string title, string description, DateTime dueDate, TaskPriority priority)
    {
        var task = new TaskEntity
        {
            Title = title,
            Description = description,
            DueDate = dueDate,
            Priority = priority,
            Status = TaskStatus.Pending,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        
        await _taskRepository.AddTaskAsync(task);
        return task;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllTasksForUserAsync(Guid userId)
    {
        return await _taskRepository.GetTaskByUserId(userId).ToListAsync();
    }
}