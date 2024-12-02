using System.ComponentModel.DataAnnotations;
using TaskManager.Models;

namespace TaskManager.API.DTOs;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public TaskPriority Priority { get; set; }
}