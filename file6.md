# Activity 2: Debugging and Optimization with Microsoft Copilot

## Common Issues Found and Fixed

### Issue 1: Null Reference Exception in Tasks.razor
**Problem:** When loading tasks, the tasks list was null causing NullReferenceException.
**Copilot Prompt Used:** "My Tasks.razor component throws NullReferenceException when loading tasks. Here's my OnInitializedAsync method. What's wrong?"

**Solution Provided by Copilot:**
The issue was that tasks was null when the component rendered before data loaded. Fixed by adding null checking:

```csharp
@if (tasks == null)
{
    <p><em>Loading...</em></p>
}
else
{
    // Display tasks
}