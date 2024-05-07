using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddMediatR(config => config.RegisterServicesFromAssembly());

        return services;
    }
}
