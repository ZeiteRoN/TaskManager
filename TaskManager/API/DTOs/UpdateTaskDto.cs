using System.ComponentModel.DataAnnotations;
using TaskManager.Models;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace TaskManager.API.DTOs;

public class UpdateTaskDto
{
    [Required] public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    [Required] public TaskPriority Priority { get; set; } 
    [Required] public Models.TaskStatus Status { get; set; }
}
