using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Repositories;

public class UserRepository: IUserRepository
{
    public Task<User?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}