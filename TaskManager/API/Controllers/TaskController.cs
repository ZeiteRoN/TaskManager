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

    /// <summary>
    /// Get all tasks for the authenticated user.
    /// </summary>
    /// <remarks>
    /// Retrieves a list of tasks associated with the currently logged-in user.
    /// You can apply optional filters such as status, due date, or priority.
    /// </remarks>
    /// <param name="filter">The filter criteria for tasks (status, due date, priority).</param>
    /// <response code="200">Returns the list of tasks.</response>
    /// <response code="401">If the user is not authenticated.</response>
    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] TaskFilterDto filter)
    {
        var userId = GetUserIdFromToken();
        var tasks = await _taskService.GetTasksForUserAsync(userId, filter);
        return Ok(tasks);
    }

    /// <summary>
    /// Get a task by its ID.
    /// </summary>
    /// <remarks>
    /// Retrieves the details of a specific task that belongs to the authenticated user.
    /// </remarks>
    /// <param name="id">The ID of the task.</param>
    /// <response code="200">Returns the task details.</response>
    /// <response code="404">If the task with the given ID was not found.</response>
    /// <response code="401">If the user is not authenticated.</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var userId = GetUserIdFromToken();
        var task = await _taskService.GetTaskByIdAsync(userId, id);
        return Ok(task);
    }
    
    
    /// <summary>
    /// Create a new task.
    /// </summary>
    /// <remarks>
    /// Creates a new task for the authenticated user. 
    /// The task requires a title, due date, and priority.
    /// </remarks>
    /// <param name="createTaskDto">The task data to create.</param>
    /// <response code="201">If the task was created successfully.</response>
    /// <response code="401">If the user is not authenticated.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto createTaskDto)
    {
        var userId = GetUserIdFromToken();
        var task = await _taskService.CreateTaskForUserAsync(createTaskDto, userId);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }


    /// <summary>
    /// Update an existing task.
    /// </summary>
    /// <remarks>
    /// Updates the specified task with new details. 
    /// Only the task owner can perform this operation.
    /// </remarks>
    /// <param name="taskId">The ID of the task to update.</param>
    /// <param name="dto">The updated task data.</param>
    /// <response code="200">If the task was updated successfully.</response>
    /// <response code="404">If the task with the given ID was not found.</response>
    /// <response code="401">If the user is not authenticated.</response>
    [HttpPut("{taskId}")]
    public async Task<IActionResult> Update(Guid taskId, [FromBody] UpdateTaskDto dto)
    {
        var userId = GetUserIdFromToken();
        var updatedTask = await _taskService.UpdateTaskAsync(userId, taskId, dto.Title, dto.Description, dto.DueDate, dto.Status, dto.Priority);
        return Ok(updatedTask);
    }

    /// <summary>
    /// Delete a task.
    /// </summary>
    /// <remarks>
    /// Deletes a task by its ID. 
    /// Only the task owner can perform this operation.
    /// </remarks>
    /// <param name="taskId">The ID of the task to delete.</param>
    /// <response code="204">If the task was deleted successfully.</response>
    /// <response code="404">If the task with the given ID was not found.</response>
    /// <response code="401">If the user is not authenticated.</response>
    [HttpDelete("{taskId}")]
    public async Task<IActionResult> Delete(Guid taskId)
    {
        var userId = GetUserIdFromToken();
        await _taskService.DeleteTaskAsync(userId, taskId);
        return NoContent();
    }
}


