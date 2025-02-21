namespace BackTasker.Worker.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Register the <see cref="Worker"/> service and it's dependencies for use with Dependency Injection in your application.
    /// </summary>
    public static IServiceCollection AddBackTaskerBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();
        
        return services;
    }
}