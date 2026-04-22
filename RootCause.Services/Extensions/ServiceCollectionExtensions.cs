using Microsoft.Extensions.DependencyInjection;
using RootCause.Core.Interfaces;

namespace RootCause.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IBugService, BugService>();
        return services;
    }
}
