using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Configuration;

namespace TaskManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(AppDbContext dbContext) : ControllerBase
{
    private readonly AppDbContext _dbContext = dbContext;

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        return Ok(_dbContext.Users.ToList());
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        return await Task.FromResult(Ok());
    }
}