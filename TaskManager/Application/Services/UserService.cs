using TaskManager.API.DTOs;
using TaskManager.API.Exceptions;
using TaskManager.Application.Helpers;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Models;

namespace TaskManager.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="email">The email of the user.</param>
    /// <param name="password">The plaintext password of the user.</param>
    /// <returns>The created user entity.</returns>
    public async Task<UserEntity> RegisterAsync(string username, string email, string password)
    {
        if (await _userRepository.ExistByEmailOrUsernameAsync(email, username))
        {
            throw new DuplicateUserException("A user with the same email or username already exists.");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            throw new ValidationException("Password must be at least 8 characters long.");
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

    /// <summary>
    /// Authenticates a user and generates a JWT token.
    /// </summary>
    /// <param name="loginDto">The login data (username and password).</param>
    /// <returns>A JWT token if authentication is successful.</returns>
    public async Task<string?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        return _jwtService.GenerateJwtToken(user);
    }
}
