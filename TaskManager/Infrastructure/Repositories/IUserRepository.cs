using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
}