using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<TaskEntity?> GetTaskByIdAsync(Guid taskId);
    IQueryable<TaskEntity?> GetTaskByUserId(Guid userId); 
    Task AddTaskAsync(TaskEntity task);
    Task UpdateTaskAsync(TaskEntity task);
    Task DeleteTaskAsync(TaskEntity task);
}