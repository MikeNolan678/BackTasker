namespace BackTasker.Core;

/// <summary>
/// Represents a recurring schedule, which is applied to a task.
/// </summary>
public class RecurringSchedule
{
    /// <summary>
    /// The start date of the recurring schedule. The task will not be performed before this date, and will start at this time.
    /// </summary>
    public DateTime StartDate { get; init; }
    
    /// <summary>
    /// The end date of the recurring schedule. The task will not be performed after this date.
    /// If this is null, the task will be performed indefinitely.
    /// </summary>
    public DateTime? EndDate { get; init; }
    
    /// <summary>
    /// The total execution count of the recurring schedule. The task will be performed this many times.
    /// If this is null, the task will be performed indefinitely.
    /// </summary>
    public int? TotalExecutions { get; init; }
    
    /// <summary>
    /// The current execution count of the recurring schedule. The task has been performed this many times.
    /// </summary>
    public int ExecutionCount { get; set; }
    
    /// <summary>
    /// The remaining execution count of the recurring schedule. This is the number of times the task can still be performed.
    /// </summary>
    public int? RemainingExecutions => TotalExecutions - ExecutionCount;
    
    /// <summary>
    /// The interval of the recurring schedule. The task will be performed every interval.
    /// <br />
    /// The configured interval should also consider the polling time set when adding Back Tasker to the service collection.
    /// <br />
    /// <example>
    /// If the polling time is set to 10 seconds and the interval is set to 5 seconds,
    /// the task will be performed every 10 seconds, as the polling time is greater than the interval.
    /// </example>
    /// </summary>
    public TimeSpan Interval { get; init; }
    
    /// <summary>
    /// Validates the recurring schedule.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when an invalid configuration is applied to the RecurringSchedule.</exception>
    public void Validate()
    {
        if (StartDate == default)
        {
            throw new ArgumentException($"The {nameof(StartDate)} must be specified.");
        }
        
        if (EndDate.HasValue && EndDate.Value < StartDate)
        {
            throw new ArgumentException($"The {nameof(EndDate)} must be after the start date.");
        }
        
        if (TotalExecutions.HasValue && TotalExecutions.Value < 1)
        {
            throw new ArgumentException($"The {nameof(TotalExecutions)} must be greater than zero.");
        }
        
        if (Interval <= TimeSpan.Zero)
        {
            throw new ArgumentException("The interval must be greater than zero.");
        }
    }
    
    /// <summary>
    /// Checks if the schedule is within the bounds configured by the recurring schedule.
    /// </summary>
    /// <param name="currentDateTime">The current DateTime, used to calculate start and end date logic.</param>
    /// <param name="lastExecutionDateTime">The last execution date of the task.</param>
    /// <returns></returns>
    internal bool IsInScheduledBounds(DateTime currentDateTime, DateTime? lastExecutionDateTime)
    {
        return IsInDateBounds(currentDateTime) && IsInExecutionCountBounds() && IsInIntervalBounds(currentDateTime, lastExecutionDateTime);
    }
    
    private bool IsInDateBounds(DateTime currentDateTime)
    {
        return StartDate <= currentDateTime && (EndDate == null || EndDate >= currentDateTime);
    }
    
    private bool IsInExecutionCountBounds()
    {
        return TotalExecutions is null || ExecutionCount < TotalExecutions;
    }
    
    private bool IsInIntervalBounds(DateTime currentDateTime, DateTime? lastExecutionDateTime)
    {
        return lastExecutionDateTime is null || lastExecutionDateTime.Value.Add(Interval) <= currentDateTime;
    }
}