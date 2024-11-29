using TaskManager.Models;
using TaskStatus = TaskManager.Models.TaskStatus;

namespace TaskManager.API.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public TaskStatus Status { get; set; }
    
    public TaskPriority Priority { get; set; }
}