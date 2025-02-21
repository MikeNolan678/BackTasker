using BackTasker.Core;

namespace BackTasker.Data.TaskStorage;

/// <summary>
/// Represents the storage for Background Tasks.
/// </summary>
public interface ITaskStorage
{
    /// <summary>
    /// Save a new task.
    /// </summary>
    Task Save(BackgroundTask backgroundTask);
    
    /// <summary>
    /// Load queued tasks.
    /// </summary>
    Task<IEnumerable<BackgroundTask>> LoadQueuedTasks();
    
    /// <summary>
    /// Load Completed tasks.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BackgroundTask>> LoadCompletedTasks();
    
    /// <summary>
    /// Update an existing task.
    /// </summary>
    Task UpdateTask(BackgroundTask backgroundTask);
}