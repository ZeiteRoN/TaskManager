using Microsoft.EntityFrameworkCore;
using TaskManager.Configuration;
using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{

    public async Task<UserEntity?> GetUserByIdAsync(int userId)
    {
        return await context.Users.FindAsync(userId);
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserEntity?> GetUserByUsernameAsync(string username)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> ExistByEmailOrUsernameAsync(string email, string username)
    {
        return await context.Users
            .AnyAsync(u => u.Email == email || u.Username == username);
    }

    public async Task AddUserAsync(UserEntity userEntity)
    {
        context.Users.Add(userEntity);
        await context.SaveChangesAsync();
    }
}