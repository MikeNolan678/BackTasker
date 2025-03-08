using BackTasker.Core;
using BackTasker.Services.Extensions;
using BackTasker.Services.Scheduler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackTasker.Demo;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddBackTaskerScheduler();
                services.AddBackTaskerBackgroundServices();
            })
            .Build();
        
        var backgroundTasks = host.Services.GetService<BackgroundTasks>();
        
        backgroundTasks?.Enqueue(() => SayHelloEnqueue());
        Console.WriteLine("Task enqueued.");
        
        backgroundTasks?.Schedule(() => SayHelloSchedule(), TimeSpan.FromSeconds(30));
        Console.WriteLine("Task scheduled.");
        
        backgroundTasks?.Recurring(() => SayHelloRecurring(), new RecurringSchedule()
        {
            Interval = TimeSpan.FromSeconds(10),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMinutes(10),
            TotalExecutions = 5
        });
        Console.WriteLine("Task scheduled.");
        
        await host.RunAsync();
    }
    
    static void SayHelloEnqueue()
    {
        Console.WriteLine("Hello, World Enqueue!");
    }
    static void SayHelloSchedule()
    {
        Console.WriteLine("Hello, World Schedule!");
    }
    static void SayHelloRecurring()
    {
        Console.WriteLine("Hello, World Recurring!");
    }
}