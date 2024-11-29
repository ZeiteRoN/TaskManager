using TaskManager.Models;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace TaskManager.API.DTOs;

public class UpdateTaskDto
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public TaskStatus Status { get; set; }
    
    public TaskPriority Priority { get; set; }
}