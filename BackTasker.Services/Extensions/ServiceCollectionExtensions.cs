using BackTasker.Data.TaskQueue;
using BackTasker.Data.TaskStorage;
using BackTasker.Services.BackgroundWorker;
using BackTasker.Services.Scheduler;
using BackTasker.Services.TaskProcessing;
using BackTasker.Services.TaskQueue;
using Microsoft.Extensions.DependencyInjection;
using TaskScheduler = BackTasker.Services.Scheduler.TaskScheduler;

namespace BackTasker.Services.Extensions;

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
        services.AddSingleton<ITaskQueue, InMemoryQueue>();
        
        services.AddScoped<ITaskRunner, TaskRunner>();
        services.AddScoped<ITaskQueueManager, TaskQueueManager>();
        
        services.AddScoped<ITaskScheduler, TaskScheduler>();
        services.AddScoped<BackgroundTasks>();
        
        return services;
    }
    
    /// <summary>
    /// Register the <see cref="Worker"/> service and it's dependencies for use with Dependency Injection in your application.
    /// </summary>
    public static IServiceCollection AddBackTaskerBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();
        
        return services;
    }
    
    //TODO: Add extensions for configuring the storage provider
}