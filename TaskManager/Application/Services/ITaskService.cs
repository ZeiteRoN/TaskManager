using TaskManager.Models;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.Application.Services;

public interface ITaskService
{
    Task<TaskEntity> CreateTaskForUserAsync(Guid userId, string title, string description, DateTime dueDate, TaskPriority priority);
    Task<IEnumerable<TaskEntity>> GetAllTasksForUserAsync(Guid userId);
    Task<TaskEntity> GetTaskByIdAsync(Guid userId, Guid taskId);
    Task<TaskEntity> UpdateTaskAsync(Guid userId, Guid taskId, string title, string description, DateTime dueDate, TaskStatus taskStatus, TaskPriority priority);
    Task DeleteTaskAsync(Guid userId, Guid taskId);
}