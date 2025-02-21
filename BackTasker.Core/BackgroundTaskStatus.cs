namespace BackTasker.Core;

/// <summary>
/// Represents the status of a background task.
/// </summary>
public enum BackgroundTaskStatus
{
    Queued = 0,
    InProgress = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4
}