using BackTasker.Data.TaskStorage;
using BackTasker.Scheduler.Schedule;
using Microsoft.Extensions.DependencyInjection;
using TaskScheduler = BackTasker.Scheduler.Schedule.TaskScheduler;

namespace BackTasker.Scheduler.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register the <see cref="BackgroundTasks"/> service and it's dependencies for use with Dependency Injection in your application.
    /// </summary>
    public static IServiceCollection AddBackTaskerScheduler(this IServiceCollection services)
    {
        //TODO: For testing purposes
        services.AddSingleton<ITaskStorage, InMemoryStorage>();
        
        services.AddSingleton<ITaskScheduler, TaskScheduler>();
        services.AddSingleton<BackgroundTasks>();
        
        return services;
    }
    
    //TODO: Add extensions for configuring the storage provider
}