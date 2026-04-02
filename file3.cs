using BlazorCopilotProject.Models;

namespace BlazorCopilotProject.Services;

public class TaskService : ITaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public TaskService()
    {
        // Seed sample data
        _tasks.Add(new TaskItem { Id = _nextId++, Title = "Complete Blazor Project", Description = "Finish all three activities", IsComplete = false, DueDate = DateTime.Now.AddDays(7), Priority = 5 });
        _tasks.Add(new TaskItem { Id = _nextId++, Title = "Review Documentation", Description = "Read Blazor documentation", IsComplete = false, DueDate = DateTime.Now.AddDays(3), Priority = 3 });
        _tasks.Add(new TaskItem { Id = _nextId++, Title = "Test Application", Description = "Run and test all features", IsComplete = true, DueDate = DateTime.Now.AddDays(-2), Priority = 4 });
    }

    public Task<List<TaskItem>> GetAllTasksAsync()
    {
        return Task.FromResult(_tasks.ToList());
    }

    public Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task AddTaskAsync(TaskItem task)
    {
        task.Id = _nextId++;
        task.CreatedDate = DateTime.Now;
        _tasks.Add(task);
        return Task.CompletedTask;
    }

    public Task UpdateTaskAsync(TaskItem updatedTask)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
        if (existingTask != null)
        {
            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.IsComplete = updatedTask.IsComplete;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.Priority = updatedTask.Priority;
        }
        return Task.CompletedTask;
    }

    public Task DeleteTaskAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            _tasks.Remove(task);
        }
        return Task.CompletedTask;
    }
}