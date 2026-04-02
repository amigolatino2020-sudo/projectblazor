@page "/tasks"
@using BlazorCopilotProject.Models
@using BlazorCopilotProject.Services
@inject ITaskService TaskService
@inject NavigationManager Navigation

<h3>Task Management</h3>

<button class="btn btn-primary mb-3" @onclick="ShowAddModal">
    <i class="bi bi-plus-circle"></i> Add New Task
</button>

@if (tasks == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <table class="table table-striped table-hover">
        <thead>
            <tr>
                <th>Title</th>
                <th>Description</th>
                <th>Due Date</th>
                <th>Priority</th>
                <th>Status</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var task in tasks)
            {
                <tr class="@(task.IsComplete ? "table-success" : "")">
                    <td>@task.Title</td>
                    <td>@task.Description</td>
                    <td>@(task.DueDate?.ToShortDateString() ?? "Not set")</td>
                    <td>
                        <span class="badge @GetPriorityBadgeClass(task.Priority)">
                            @GetPriorityText(task.Priority)
                        </span>
                    </td>
                    <td>
                        <input type="checkbox" @bind="task.IsComplete" @onchange="() => UpdateTask(task)" />
                        <span>@(task.IsComplete ? "Completed" : "Pending")</span>
                    </td>
                    <td>
                        <button class="btn btn-sm btn-warning me-1" @onclick="() => EditTask(task.Id)">
                            <i class="bi bi-pencil"></i> Edit
                        </button>
                        <button class="btn btn-sm btn-danger" @onclick="() => DeleteTask(task.Id)">
                            <i class="bi bi-trash"></i> Delete
                        </button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    private List<TaskItem>? tasks;
    private TaskItem? selectedTask;
    private bool showModal = false;

    protected override async Task OnInitializedAsync()
    {
        await LoadTasks();
    }

    private async Task LoadTasks()
    {
        tasks = await TaskService.GetAllTasksAsync();
    }

    private void ShowAddModal()
    {
        selectedTask = new TaskItem();
        showModal = true;
    }

    private async Task EditTask(int id)
    {
        selectedTask = await TaskService.GetTaskByIdAsync(id);
        showModal = true;
    }

    private async Task SaveTask()
    {
        if (selectedTask != null)
        {
            if (selectedTask.Id == 0)
            {
                await TaskService.AddTaskAsync(selectedTask);
            }
            else
            {
                await TaskService.UpdateTaskAsync(selectedTask);
            }
            await LoadTasks();
            showModal = false;
            selectedTask = null;
        }
    }

    private async Task UpdateTask(TaskItem task)
    {
        await TaskService.UpdateTaskAsync(task);
        await LoadTasks();
    }

    private async Task DeleteTask(int id)
    {
        if (await TaskService.GetTaskByIdAsync(id) != null)
        {
            await TaskService.DeleteTaskAsync(id);
            await LoadTasks();
        }
    }

    private string GetPriorityBadgeClass(int priority)
    {
        return priority switch
        {
            5 => "bg-danger",
            4 => "bg-warning text-dark",
            3 => "bg-info text-dark",
            2 => "bg-secondary",
            _ => "bg-success"
        };
    }

    private string GetPriorityText(int priority)
    {
        return priority switch
        {
            5 => "Critical",
            4 => "High",
            3 => "Medium",
            2 => "Low",
            _ => "Very Low"
        };
    }
}