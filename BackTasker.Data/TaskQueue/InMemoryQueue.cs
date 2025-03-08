using System.Collections.Concurrent;
using BackTasker.Core;

namespace BackTasker.Data.TaskQueue;

/// <summary>
/// Represents an in-memory queue for Background Tasks.
/// </summary>
public class InMemoryQueue : ITaskQueue
{
    //TODO: Add multiple queues for different priorities
    private readonly ConcurrentQueue<BackgroundTask> _tasks = new();
    
    /// <summary>
    /// Queue a new task.
    /// </summary>
    /// <param name="backgroundTask">The background task to queue.</param>
    public void QueueTask(BackgroundTask backgroundTask)
    {
        _tasks.Enqueue(backgroundTask);
    }
    
    /// <summary>
    /// Dequeue a task from the beginning of the queue.
    /// </summary>
    /// <returns>A background task from the queue, or null if no tasks are queued.</returns>
    public BackgroundTask? DequeueTask()
    {
        if (_tasks.TryDequeue(out var task))
        {
            return task;
        }
        
        return null;
    }
    
    /// <summary>
    /// Check if the queue is empty.
    /// </summary>
    /// <returns>A boolean indicating if the queue is empty.</returns>
    public bool IsEmpty()
    {
        return _tasks.IsEmpty;
    }
}