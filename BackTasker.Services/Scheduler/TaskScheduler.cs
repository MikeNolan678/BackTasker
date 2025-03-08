using System.Linq.Expressions;
using BackTasker.Core;
using BackTasker.Data.TaskStorage;

namespace BackTasker.Services.Scheduler;

/// <summary>
/// Enqueue, schedule, or schedule a recurring background task.
/// </summary>
internal sealed class TaskScheduler : ITaskScheduler
{
    private readonly ITaskStorage _taskStorage;
    
    public TaskScheduler(ITaskStorage taskStorage)
    {
        _taskStorage = taskStorage;
    }
    
    /// <summary>
    /// Enqueues a task to be performed as soon as possible.
    /// </summary>
    /// <param name="method">The method to perform.</param>
    /// <param name="priority">The priority of the task.</param>
    /// <returns>A task.</returns>
    public async Task Enqueue(Expression<Action> method, TaskPriority priority)
    {
        BackgroundTask task = GenerateBackgroundTask(BackgroundTaskType.Enqueue, method, priority);
        
        await _taskStorage.Save(task);
    }
    
    /// <summary>
    /// Schedules a task to be performed after a delay.
    /// </summary>
    /// <param name="method">The method to perform.</param>
    /// <param name="delay">The delay before the method will be called.</param>
    /// <param name="priority">The priority of the task.</param>
    /// <returns>A task.</returns>
    public async Task Schedule(Expression<Action> method, TimeSpan delay, TaskPriority priority)
    {
        BackgroundTask task = GenerateBackgroundTask(BackgroundTaskType.Schedule, method, priority);
        
        task.Delay = delay;
        
        await _taskStorage.Save(task);
    }
    
    /// <summary>
    /// Schedules a task to be performed on the specified schedule.
    /// </summary>
    /// <param name="method">The method to perform.</param>
    /// <param name="recurringSchedule">The recurring schedule to apply to the recurring task.</param>
    /// <param name="priority">The priority of the task.</param>
    /// <returns>A task.</returns>
    public async Task Recurring(Expression<Action> method, RecurringSchedule recurringSchedule, TaskPriority priority)
    {
        BackgroundTask task = GenerateBackgroundTask(BackgroundTaskType.Recurring, method, priority);
        
        task.RecurringSchedule = recurringSchedule;
        
        await _taskStorage.Save(task);
    }
    
    /// <summary>
    /// Generates a background task from an expression.
    /// </summary>
    /// <param name="taskType">The task type.</param>
    /// <param name="method">The method.</param>
    /// <param name="priority">The priority.</param>
    /// <returns>A Background Task.</returns>
    private BackgroundTask GenerateBackgroundTask(BackgroundTaskType taskType, Expression<Action> method,
        TaskPriority priority)
    {
        if (method.Body is not MethodCallExpression methodCall)
            throw new ArgumentException("Invalid method call.");
        
        return new BackgroundTask()
        {
            BackgroundTaskType = taskType,
            ClassName = methodCall.Method.DeclaringType?.FullName,
            MethodName = methodCall.Method.Name,
            Arguments = methodCall.Arguments
                .Select(arg => Expression.Lambda(arg).Compile().DynamicInvoke()?.ToString() ?? "null")
                .ToArray(), // TODO: Improve this logic to handle more complex types, such as objects.
            BackgroundTaskStatus = BackgroundTaskStatus.Waiting,
            Id = Guid.NewGuid(),
            Priority = priority,
            CreatedDateUtc = DateTime.UtcNow
        };
    }
}