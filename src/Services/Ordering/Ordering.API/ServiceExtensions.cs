namespace Ordering.API;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // AddCarter

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // MapCarter

        return app;
    }
}
