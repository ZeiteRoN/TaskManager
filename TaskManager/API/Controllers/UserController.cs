using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.API.Exceptions;
using TaskManager.Application.Services;
using TaskManager.Configuration;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <remarks>
    /// This method allows creating a new user by providing a unique username, email, and password.
    /// </remarks>
    /// <param name="registerDto">The user registration data.</param>
    /// <response code="201">If the user was successfully registered.</response>
    /// <response code="400">If the request contains invalid data.</response>

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var user = await _userService.RegisterAsync(registerDto.Username, registerDto.Email, registerDto.Password);
            return StatusCode(201, new { Message = "User registered successfully.", UserId = user.Id });
        }
        catch (DuplicateUserException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    /// <summary>
    /// Authenticate a user and return a JWT token.
    /// </summary>
    /// <remarks>
    /// This method validates the user's credentials and returns a JWT token that can be used for authenticated requests.
    /// </remarks>
    /// <param name="loginDto">The user login data (username and password).</param>
    /// <response code="200">Returns the JWT token if authentication is successful.</response>
    /// <response code="401">If the credentials are invalid.</response>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var token = await _userService.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
    }
}
