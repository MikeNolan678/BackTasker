using BackTasker.Services.TaskQueue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackTasker.Services.BackgroundWorker;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    
    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    // TODO: This implementation is just to get the scheduler working.
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // TODO: The delay applied here needs to be configurable.
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            
            using var scope = _serviceProvider.CreateScope();
            
            var taskQueueService = scope.ServiceProvider.GetRequiredService<ITaskQueueManager>();
            
            await taskQueueService.QueueWaitingTasks();
            
            taskQueueService.ExecuteQueuedTasks();
        }
    }
}