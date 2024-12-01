using TaskManager.Models;

namespace TaskManager.Application.Services;

public interface ITaskService
{
    Task<TaskEntity> CreateTaskForUserAsync(Guid userId, string title, string description, DateTime dueDate, TaskPriority priority);
    Task<IEnumerable<TaskEntity>> GetAllTasksForUserAsync(Guid userId);
}