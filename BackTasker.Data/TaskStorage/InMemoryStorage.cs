using System.Collections.Concurrent;
using BackTasker.Core;

namespace BackTasker.Data.TaskStorage;

/// <summary>
/// InMemory storage for Background Tasks testing, will not persist data.
/// </summary>
public class InMemoryStorage : ITaskStorage
{
    private readonly ConcurrentDictionary<Guid, BackgroundTask> _tasks = new();
    
    public InMemoryStorage()
    {
        //TODO: For testing purposes only.
    }
    
    public Task Save(BackgroundTask backgroundTask)
    {
        if (_tasks.TryAdd(backgroundTask.Id ?? Guid.NewGuid(), backgroundTask) == false)
        {
            Console.WriteLine("Failed to save task.");
        }
        
        return Task.CompletedTask;
    }
    
    public Task<IEnumerable<BackgroundTask>> LoadWaitingTasks()
    {
        var waitingTasks = _tasks.Values.Where(t => t.BackgroundTaskStatus == BackgroundTaskStatus.Waiting);
        
        return Task.FromResult(waitingTasks);
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