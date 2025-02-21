using System.Linq.Expressions;
using BackTasker.Core;

namespace BackTasker.Scheduler.Schedule;

/// <summary>
/// Enqueue, schedule, or schedule a recurring background task.
/// </summary>
public interface ITaskScheduler
{
    /// <summary>
    /// Enqueues a task to be performed as soon as possible.
    /// </summary>
    /// <param name="method">The method to perform.</param>
    /// <param name="priority">The priority of the task.</param>
    /// <returns>A task.</returns>
    Task Enqueue(Expression<Action> method, TaskPriority priority);
    
    /// <summary>
    /// Schedules a task to be performed after a delay.
    /// </summary>
    /// <param name="method">The method to perform.</param>
    /// <param name="delay">The delay before the method will be called.</param>
    /// <param name="priority">The priority of the task.</param>
    /// <returns>A task.</returns>
    Task Schedule(Expression<Action> method, TimeSpan delay, TaskPriority priority);
    
    /// <summary>
    /// Schedules a task to be performed on the specified schedule.
    /// </summary>
    /// <param name="method">The method to perform.</param>
    /// <param name="schedule">The schedule to apply to the recurring task.</param>
    /// <param name="priority">The priority of the task.</param>
    /// <returns>A task.</returns>
    Task Recurring(Expression<Action> method, RecurringSchedule schedule, TaskPriority priority);
}