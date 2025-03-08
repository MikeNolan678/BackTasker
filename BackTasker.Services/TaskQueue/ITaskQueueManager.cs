namespace BackTasker.Services.TaskQueue;

public interface ITaskQueueManager
{
    Task QueueWaitingTasks();
    
    void ExecuteQueuedTasks();
}