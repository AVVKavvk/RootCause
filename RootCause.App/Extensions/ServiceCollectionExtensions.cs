using Microsoft.Extensions.DependencyInjection;
using RootCause.App.ViewModels;

namespace RootCause.App.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<BugListViewModel>();
        services.AddTransient<StatsViewModel>();
        return services;
    }
}
