using Microsoft.AspNetCore.Http.HttpResults;
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

    public async Task<TaskEntity> GetTaskByIdAsync(Guid userId, Guid taskId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);
        if (task == null || task.UserId != userId)
        {
            throw new UnauthorizedAccessException("Access denied or task not found");
        }
        return task;
    }

    public async Task<TaskEntity> UpdateTaskAsync(Guid userId, Guid taskId, string title, string description, DateTime dueDate,
        TaskStatus taskStatus, TaskPriority priority)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);
        if (task == null || task.UserId != userId)
        {
            throw new UnauthorizedAccessException("Access denied or task not found");
        }
        
        task.Title = title;
        task.Description = description;
        task.DueDate = dueDate;
        task.Priority = priority;
        task.Status = taskStatus;
        await _taskRepository.UpdateTaskAsync(task);
        
        return task;
    }

    public async Task DeleteTaskAsync(Guid userId, Guid taskId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(taskId);
        if (task == null || task.UserId != userId)
        {
            throw new UnauthorizedAccessException("Access denied or task not found");
        }
        await _taskRepository.DeleteTaskAsync(task);
    }
}