using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RootCause.Core.Interfaces;
using RootCause.Data.Context;
using RootCause.Data.Repositories;

namespace RootCause.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<BugDBContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IBugRepository, BugRepository>();

        return services;
    }
}
