using TaskManager.API.DTOs;
using TaskManager.Application.Helpers;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Models;

namespace TaskManager.Application.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<UserEntity> RegisterAsync(string username, string email, string password)
    {
        if (await _userRepository.ExistByEmailOrUsernameAsync(email, username))
        {
            throw new Exception("User already exists!");
        }
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new UserEntity
        {
            Username = username,
            Email = email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddUserAsync(user);
        return user;
    }

    public async Task<string?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        return _jwtService.GenerateJwtToken(user);
    }
}