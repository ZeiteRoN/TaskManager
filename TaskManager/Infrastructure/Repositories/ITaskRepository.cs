namespace TaskManager.Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<Task>> GetAllTasksAsync(Guid userId);
    Task<Task?> GetTaskByIdAsync(Guid taskId);
    Task AddTaskAsync(Task task);
    Task UpdateTaskAsync(Task task);
    Task DeleteTaskAsync(Task task);
}