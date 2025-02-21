using System.Linq.Expressions;
using BackTasker.Core;
using Task = System.Threading.Tasks.Task;

namespace BackTasker.Scheduler.Schedule;

/// <summary>
/// Enqueue, schedule, or schedule a recurring background task.
/// </summary>
public sealed class BackgroundTasks
{
    private readonly ITaskScheduler _taskScheduler;
    
    public BackgroundTasks(ITaskScheduler taskScheduler)
    {
        _taskScheduler = taskScheduler;
    }
    
    /// <summary>
    /// Enqueues a task to be performed as soon as possible.
    /// </summary>
    /// <param name="method">The method to perform.</param>
    /// <param name="priority">(optional) The priority of the task. The default priority is <see cref="TaskPriority.Medium"/></param>
    /// <returns>A task.</returns>
    public Task Enqueue(Expression<Action> method, TaskPriority priority = TaskPriority.Medium)
    {
        return _taskScheduler.Enqueue(method, priority);
    }
    
    /// <summary>
    /// Schedules a task to be performed after a delay.
    /// </summary>
    /// <param name="method">The method to perform.</param>
    /// <param name="delay">The delay before the method will be called.</param>
    /// <param name="priority">(optional) The priority of the task. The default priority is <see cref="TaskPriority.Medium"/></param>
    /// <returns>A task.</returns>
    public Task Schedule(Expression<Action> method, TimeSpan delay, TaskPriority priority = TaskPriority.Medium)
    {
        return _taskScheduler.Schedule(method, delay, priority);
    }
    
    /// <summary>
    /// Schedules a task to be performed on the specified schedule.
    /// </summary>
    /// <param name="method">The method to perform.</param>
    /// <param name="schedule">The schedule to apply to the recurring task.</param>
    /// <param name="priority">(optional) The priority of the task. The default priority is <see cref="TaskPriority.Medium"/></param>
    /// <returns>A task.</returns>
    public Task Recurring(Expression<Action> method, RecurringSchedule schedule, TaskPriority priority = TaskPriority.Medium)
    {
        schedule.Validate();
        
        return _taskScheduler.Recurring(method, schedule, priority);
    }
    
}