using BackTasker.Core;
using BackTasker.Data.TaskQueue;
using BackTasker.Data.TaskStorage;

namespace BackTasker.Services.TaskQueue;

/// <summary>Task queue manager. This manages the queued tasks.</summary>
public class TaskQueueManager : ITaskQueueManager
{
    private readonly ITaskStorage _taskStorage;
    private readonly ITaskQueue _taskQueue;
    
    public TaskQueueManager(ITaskStorage taskStorage, ITaskQueue taskQueue)
    {
        _taskStorage = taskStorage;
        _taskQueue = taskQueue;
    }
    
    /// <summary>
    /// Queue waiting tasks.
    /// This method will queue tasks that are waiting to be executed, based on the task type and schedule.
    /// Tasks will be queued based on the configured polling time. 
    /// </summary>
    public async Task QueueWaitingTasks()
    {
        var waitingTasks = await _taskStorage.LoadWaitingTasks();
        
        var tasksToQueue = waitingTasks
                .Where(t => t.ShouldQueue(DateTime.UtcNow))
                .OrderBy(t => t.CreatedDateUtc);
        
        foreach (var task in tasksToQueue)
        {
            _taskQueue.QueueTask(task);
            Console.WriteLine($"Queued task: {task.MethodName}");
        }
    }
    
    /// <summary>
    /// Execute queued tasks. This will run until the task queue is empty.
    /// This method will execute tasks in the queue, and handle the task completion or failure.
    /// Tasks will be updated after execution.
    /// </summary>
    public void ExecuteQueuedTasks()
    {
        while (_taskQueue.IsEmpty() == false)
        {
            var task = _taskQueue.DequeueTask();
            
            if (task is not null)
            {
                try
                {
                    Execute(task);
                    HandleTaskCompletion(task);
                }
                catch (Exception exception)
                {
                    HandleTaskFailure(task, exception);
                }
            }
        }
    }
    
    /// <summary>
    /// Execute a task. This will run the task and update the last execution date.
    /// </summary>
    /// <param name="task">The task to execute.</param>
    private void Execute(BackgroundTask task)
    {
        // TODO: Execute tasks in queue, using the task runner
        Console.WriteLine($"Executing task: {task.MethodName}");
    }
    
    /// <summary>
    /// Handle task completion. This will update the task status, execution count and last execution DateTime.
    /// </summary>
    /// <param name="task"></param>
    private void HandleTaskCompletion(BackgroundTask task)
    {
        if (task is { BackgroundTaskType: BackgroundTaskType.Recurring, RecurringSchedule: not null })
        {
            task.RecurringSchedule.ExecutionCount++;
            task.LastExecutionDateUtc = DateTime.UtcNow;
            
            if (task.RecurringSchedule.RemainingExecutions == 0)
            {
                task.BackgroundTaskStatus = BackgroundTaskStatus.Completed;
            }
            else
            {
                task.BackgroundTaskStatus = BackgroundTaskStatus.Waiting;
            }
        }
        else
        {
            task.BackgroundTaskStatus = BackgroundTaskStatus.Completed;
        }
        
        _taskStorage.UpdateTask(task);
    }
    
    /// <summary>
    /// Handle task failure. This will log the failure and update the task status.
    /// </summary>
    /// <param name="task">The failed task.</param>
    /// <param name="exception">The exception which was thrown when the task failed.</param>
    private void HandleTaskFailure(BackgroundTask task, Exception exception)
    {
        // TODO: Log the failure + the exception details.
        
        if (task is { BackgroundTaskType: BackgroundTaskType.Recurring, RecurringSchedule: not null })
        {
            task.RecurringSchedule.ExecutionCount++;
            
            if (task.RecurringSchedule.RemainingExecutions == 0)
            {
                task.BackgroundTaskStatus = BackgroundTaskStatus.Failed;
            }
            else
            {
                task.BackgroundTaskStatus = BackgroundTaskStatus.Waiting;
            }
        }
        else
        {
            task.BackgroundTaskStatus = BackgroundTaskStatus.Failed;
        }
        
        _taskStorage.UpdateTask(task);
    }
}