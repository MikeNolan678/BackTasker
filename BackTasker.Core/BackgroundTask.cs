namespace BackTasker.Core;

/// <summary>
/// Represents a background task.
/// </summary>
public class BackgroundTask
{
    /// <summary>
    /// The unique identifier of the task.
    /// </summary>
    public Guid? Id { get; init; }
    
    /// <summary>
    /// The type of the task.
    /// </summary>
    public BackgroundTaskType BackgroundTaskType { get; init; }
    
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
    
    /// <summary>
    /// The date the task was created in UTC.
    /// </summary>
    public DateTime CreatedDateUtc { get; init; }
    
    /// <summary>
    /// The date the task was last executed in UTC.
    /// </summary>
    public DateTime? LastExecutionDateUtc { get; set; }
    
    /// <summary>
    /// Indicates whether the task should be queued, based on the task type, configuration, and current date/time.
    /// </summary>
    /// <param name="currentDateTime">The current DateTime.</param>
    /// <returns>A boolean indicating whether the task should queue.</returns>
    public bool ShouldQueue(DateTime currentDateTime)
    {
        return (BackgroundTaskType) switch
        {
            BackgroundTaskType.Enqueue => ShouldQueueEnqueuedTask(),
            BackgroundTaskType.Schedule => ShouldQueueScheduledTask(currentDateTime),
            BackgroundTaskType.Recurring => ShouldQueueRecurringTask(currentDateTime),
            _ => false
        };
    }
    
    private bool ShouldQueueEnqueuedTask()
    {
        return BackgroundTaskStatus == BackgroundTaskStatus.Waiting;
    }
    
    private bool ShouldQueueScheduledTask(DateTime currentDateTime)
    {
        return BackgroundTaskStatus == BackgroundTaskStatus.Waiting
               && Delay is not null
               && CreatedDateUtc.Add((TimeSpan)Delay) <= currentDateTime;
    }
    
    private bool ShouldQueueRecurringTask(DateTime currentDateTime)
    {
        return BackgroundTaskStatus == BackgroundTaskStatus.Waiting
               && RecurringSchedule is not null
               && RecurringSchedule.IsInScheduledBounds(currentDateTime, LastExecutionDateUtc);
    }
}