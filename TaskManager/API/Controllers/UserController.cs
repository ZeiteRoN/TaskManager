using Microsoft.AspNetCore.Mvc;
using TaskManager.Configuration;

namespace TaskManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(AppDbContext dbContext) : ControllerBase
{
    private readonly AppDbContext _dbContext = dbContext;

    public IActionResult GetAllUsers()
    {
        return Ok(_dbContext.Users.ToList());
    }
}