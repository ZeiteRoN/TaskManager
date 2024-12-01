using TaskManager.API.DTOs;
using TaskManager.Models;

namespace TaskManager.Application.Services;

public interface IUserService
{
    Task<UserEntity> RegisterAsync(string username, string email, string password);

    Task<string?> LoginAsync(LoginDto loginDto);
}