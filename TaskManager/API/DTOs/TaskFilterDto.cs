using TaskManager.Models;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.API.DTOs;

public class TaskFilterDto
{
    public TaskStatus? Status { get; set; }
    public DateTime? DueDate { get; set; }
    public TaskPriority? Priority { get; set; }
}