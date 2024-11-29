using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models;

public class Task
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string Title { get; set; }
    
    public  string Description { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    public TaskStatus Status { get; set; }
    
    public TaskPriority Priority { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
        
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }
}