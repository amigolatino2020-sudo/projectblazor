using System.ComponentModel.DataAnnotations;

namespace BlazorCopilotProject.Models;

public class TaskItem
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
    public string? Title { get; set; }
    
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }
    
    public bool IsComplete { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    public DateTime? DueDate { get; set; }
    
    [Range(1, 5, ErrorMessage = "Priority must be between 1 and 5")]
    public int Priority { get; set; } = 3;
}