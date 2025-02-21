namespace BackTasker.Core;

/// <summary>
/// Represents a background task.
/// </summary>
public class BackgroundTask
{
    /// <summary>
    /// The unique identifier of the task.
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// The type of the task.
    /// </summary>
    public BackgroundTaskType BackgroundTaskType { get; set; }
    
    /// <summary>
    /// The current status of the task.
    /// </summary>
    public BackgroundTaskStatus BackgroundTaskStatus { get; set; }
    
    /// <summary>
    /// The class name of the task.
    /// </summary>
    public string? ClassName { get; set; }
    
    /// <summary>
    /// The method name of the task.
    /// </summary>
    public string? MethodName { get; set; }
    
    /// <summary>
    /// The arguments of the method.
    /// </summary>
    public string[]? Arguments { get; set; }
    
    /// <summary>
    /// The priority of the task.
    /// </summary>
    public TaskPriority Priority { get; set; }
    
    /// <summary>
    /// The recurring schedule of the task.
    /// </summary>
    public RecurringSchedule? RecurringSchedule { get; set; }
    
    /// <summary>
    /// The delay before the task is executed?
    /// </summary>
    public TimeSpan? Delay { get; set; }
}