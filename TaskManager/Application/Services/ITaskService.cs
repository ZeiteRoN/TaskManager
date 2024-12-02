using TaskManager.API.DTOs;
using TaskManager.Models;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.Application.Services;

public interface ITaskService
{
    Task<TaskEntity> CreateTaskForUserAsync(CreateTaskDto createTaskDto, Guid userId);
    Task<IEnumerable<TaskDto>> GetTasksForUserAsync(Guid userId, TaskFilterDto filter);
    Task<TaskDto?> GetTaskByIdAsync(Guid userId, Guid taskId);
    Task<TaskEntity> UpdateTaskAsync(Guid userId, Guid taskId, string title, string description, DateTime dueDate, TaskStatus taskStatus, TaskPriority priority);
    Task DeleteTaskAsync(Guid userId, Guid taskId);
}