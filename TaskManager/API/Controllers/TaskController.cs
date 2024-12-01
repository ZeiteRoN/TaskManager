using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Application.Services;
using TaskManager.Configuration;

namespace TaskManager.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TaskController: ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(Guid taskId)
    {
        var userid = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var task = await _taskService.GetTaskByIdAsync(userid, taskId);
        return Ok(task);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid taskId, UpdateTaskDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var updatedTask = await _taskService.UpdateTaskAsync(userId, taskId, dto.Title, dto.Description, dto.DueDate, dto.Status, dto.Priority);
        return Ok(updatedTask);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _taskService.DeleteTaskAsync(userId, taskId);
        return NoContent();
    }
}
