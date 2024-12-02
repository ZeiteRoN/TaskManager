using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<UserEntity?> GetUserByIdAsync(int userId);
    
    Task<UserEntity?> GetUserByEmailAsync(string email);
    Task<UserEntity?> GetUserByUsernameAsync(string username);
    
    Task<bool> ExistByEmailOrUsernameAsync(string email, string username);
    
    Task AddUserAsync(UserEntity userEntity);
}