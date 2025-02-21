namespace BackTasker.Core;

/// <summary>
/// Represents a recurring schedule, which is applied to a task.
/// </summary>
public class RecurringSchedule
{
    /// <summary>
    /// The start date of the recurring schedule. The task will not be performed before this date, and will start at this time.
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// The end date of the recurring schedule. The task will not be performed after this date.
    /// If this is null, the task will be performed indefinitely.
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// The count of the recurring schedule. The task will be performed this many times.
    /// If this is null, the task will be performed indefinitely.
    /// </summary>
    public int? Count { get; set; }
    
    /// <summary>
    /// The interval of the recurring schedule. The task will be performed every interval.
    /// </summary>
    public TimeSpan Interval { get; set; }
    
    /// <summary>
    /// Validates the recurring schedule.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public void Validate()
    {
        if (StartDate == default)
        {
            throw new ArgumentException("The start date must be specified.");
        }
        
        if (EndDate.HasValue && EndDate.Value < StartDate)
        {
            throw new ArgumentException("The end date must be after the start date.");
        }
        
        if (Count.HasValue && Count.Value < 1)
        {
            throw new ArgumentException("The count must be greater than zero.");
        }
        
        if (Interval <= TimeSpan.Zero)
        {
            throw new ArgumentException("The interval must be greater than zero.");
        }
    }
}