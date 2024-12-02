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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TaskController(ITaskService taskService, IHttpContextAccessor httpContextAccessor)
    {
        _taskService = taskService;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize]
    [HttpGet("getTask")]
    public async Task<IActionResult> GetTaskById(Guid taskId)
    {
        var userid = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var task = await _taskService.GetTaskByIdAsync(userid, taskId);
        return Ok(task);
    }
    
    [HttpPost("addTask")]
    [Authorize]
    public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskDto createTaskDto)
    {
        try
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var task = await _taskService.CreateTaskForUserAsync(createTaskDto, Guid.Parse(userId));

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [Authorize]
    [HttpPut("updateTask")]
    public async Task<IActionResult> UpdateTask(Guid taskId, UpdateTaskDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var updatedTask = await _taskService.UpdateTaskAsync(userId, taskId, dto.Title, dto.Description, dto.DueDate, dto.Status, dto.Priority);
        return Ok(updatedTask);
    }
    
    [Authorize]
    [HttpDelete("deleteTask")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _taskService.DeleteTaskAsync(userId, taskId);
        return NoContent();
    }
}
