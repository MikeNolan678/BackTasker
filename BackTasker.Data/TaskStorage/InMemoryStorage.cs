using BackTasker.Core;

namespace BackTasker.Data.TaskStorage;

/// <summary>
/// InMemory storage for Background Tasks testing, will not persist data.
/// </summary>
public class InMemoryStorage : ITaskStorage
{
    
    private readonly Dictionary<Guid, BackgroundTask> _tasks = new();
    
    public InMemoryStorage()
    {
        //TODO: For testing purposes only.
    }
    
    public Task Save(BackgroundTask backgroundTask)
    {
        _tasks.Add(backgroundTask.Id ?? Guid.NewGuid(), backgroundTask);
        
        return Task.CompletedTask;
    }
    
    public Task<IEnumerable<BackgroundTask>> LoadQueuedTasks()
    {
        return Task.FromResult(_tasks.Values.Where(t => t.BackgroundTaskStatus == BackgroundTaskStatus.Queued));
    }
    
    public Task<IEnumerable<BackgroundTask>> LoadCompletedTasks()
    {
        return Task.FromResult(_tasks.Values.Where(t => t.BackgroundTaskStatus == BackgroundTaskStatus.Completed));
    }
    
    public Task UpdateTask(BackgroundTask backgroundTask)
    {
        _tasks[backgroundTask.Id ?? Guid.NewGuid()] = backgroundTask;
        
        return Task.CompletedTask;
    }
}