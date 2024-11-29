namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository: ITaskRepository
{
    public Task<IEnumerable<Task>> GetAllTasksAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Task?> GetTaskByIdAsync(Guid taskId)
    {
        throw new NotImplementedException();
    }

    public Task AddTaskAsync(Task task)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskAsync(Task task)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTaskAsync(Task task)
    {
        throw new NotImplementedException();
    }
}