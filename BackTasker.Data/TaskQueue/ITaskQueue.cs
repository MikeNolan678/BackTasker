using BackTasker.Core;

namespace BackTasker.Data.TaskQueue;

/// <summary>
/// Represents the queue for Background Tasks.
/// </summary>
public interface ITaskQueue
{
    /// <summary>
    /// Queue a new task.
    /// </summary>
    /// <param name="backgroundTask">The background task to queue.</param>
    void QueueTask(BackgroundTask backgroundTask);
    
    /// <summary>
    /// Dequeue a task.
    /// </summary>
    /// <returns>A background task from the queue, or null if no tasks are queued.</returns>
    BackgroundTask? DequeueTask();
    
    /// <summary>
    /// Check if the queue is empty.
    /// </summary>
    /// <returns>A boolean indicating if the queue is empty.</returns>
    bool IsEmpty();
}