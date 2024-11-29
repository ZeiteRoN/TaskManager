using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Configuration;

namespace TaskManager.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TasksController(AppDbContext dbContext) : ControllerBase
{
    private AppDbContext _dbContext = dbContext;

    [HttpGet]
    public ActionResult GetAllTasks()
    {
        return Ok(_dbContext.Tasks.ToList());
    }
}
