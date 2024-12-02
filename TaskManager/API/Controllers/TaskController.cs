using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Application.Services;
using TaskManager.Configuration;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    private Guid GetUserIdFromToken()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User ID not found in token.");
        }

        return Guid.Parse(userIdClaim);
    }

    [HttpGet("tasks/getAll")]
    public async Task<IActionResult> GetTasks([FromQuery] TaskFilterDto filter)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var tasks = await _taskService.GetTasksForUserAsync(userId, filter);

            return Ok(tasks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("tasks/getById")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var task = await _taskService.GetTaskByIdAsync(userId, id);

            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return Ok(task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("tasks/addTask")]
    public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskDto createTaskDto)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var task = await _taskService.CreateTaskForUserAsync(createTaskDto, userId);

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("tasks/updateTask")]
    public async Task<IActionResult> UpdateTask(Guid taskId, [FromBody] UpdateTaskDto dto)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var updatedTask = await _taskService.UpdateTaskAsync(userId, taskId, dto.Title, dto.Description, dto.DueDate, dto.Status, dto.Priority);

            return Ok(updatedTask);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("Tasks/deleteTask")]
    public async Task<IActionResult> Delete(Guid taskId)
    {
        try
        {
            var userId = GetUserIdFromToken();
            await _taskService.DeleteTaskAsync(userId, taskId);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}


