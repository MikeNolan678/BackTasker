namespace BackTasker.Core;

/// <summary>
/// Represents the type of background task.
/// </summary>
public enum BackgroundTaskType
{
    /// <summary>
    /// Enqueue the task and perform it as soon as possible.
    /// </summary>
    Enqueue = 0,
    
    /// <summary>
    /// Schedule the task to be performed after a specified delay.
    /// </summary>
    Schedule = 1,
    
    /// <summary>
    /// Schedule the task to be performed on a recurring schedule.
    /// </summary>
    Recurring = 2
}