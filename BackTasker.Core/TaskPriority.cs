namespace BackTasker.Core;

/// <summary>
/// Represents the priority of a task.
/// The priority determines the order in which tasks are performed.
/// If two tasks have the same priority, the order is based on the time the tasks were enqueued.
/// The default priority is <see cref="TaskPriority.Medium"/>.
/// </summary>
public enum TaskPriority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}