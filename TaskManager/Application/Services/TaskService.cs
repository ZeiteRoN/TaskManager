using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.DTOs;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Models;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskEntity> CreateTaskForUserAsync(CreateTaskDto createTaskDto, Guid userId)
    {
        var task = new TaskEntity
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            DueDate = createTaskDto.DueDate,
            UserId = userId,
            Status = TaskStatus.Pending,
            Priority = createTaskDto.Priority,
            CreatedAt = DateTime.UtcNow
        };

        await _taskRepository.AddTaskAsync(task);
        return task;
    }

    public async Task<IEnumerable<TaskDto>> GetTasksForUserAsync(Guid userId, TaskFilterDto filter)
    {
        var tasks = await _taskRepository.GetTasksByUserIdAsync(userId, filter);

        return tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            DueDate = t.DueDate,
            Status = t.Status,
            Priority = t.Priority,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        });
    }

    public async Task<TaskDto?> GetTaskByIdAsync(Guid userId, Guid taskId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(userId, taskId);

        if (task == null) return null;

        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Status = task.Status,
            Priority = task.Priority,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }

    public async Task<TaskEntity> UpdateTaskAsync(Guid userId, Guid taskId, string title, string description, DateTime dueDate,
        TaskStatus taskStatus, TaskPriority priority)
    {
        var task = await _taskRepository.GetTaskByIdAsync(userId, taskId);
        if (task == null)
        {
            throw new KeyNotFoundException("Task not found");
        }

        task.Title = title;
        task.Description = description;
        task.DueDate = dueDate;
        task.Status = taskStatus;
        task.Priority = priority;

        await _taskRepository.UpdateTaskAsync(task);

        return task;
    }

    public async Task DeleteTaskAsync(Guid userId, Guid taskId)
    {
        var task = await _taskRepository.GetTaskByIdAsync(userId, taskId);
        if (task == null)
        {
            throw new KeyNotFoundException("Task not found");
        }

        await _taskRepository.DeleteTaskAsync(task);
    }
}
