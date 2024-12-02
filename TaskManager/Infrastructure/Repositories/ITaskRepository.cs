using TaskManager.API.DTOs;
using TaskManager.Models;

namespace TaskManager.Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetTasksByUserIdAsync(Guid userId, TaskFilterDto filter);
    Task<TaskEntity?> GetTaskByIdAsync(Guid userId, Guid taskId);
    Task AddTaskAsync(TaskEntity task);
    Task UpdateTaskAsync(TaskEntity task);
    Task DeleteTaskAsync(TaskEntity task);
}