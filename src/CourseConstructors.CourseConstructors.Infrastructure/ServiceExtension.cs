using CourseConstructors.CourseConstructors.Core.Interfaces.Persistance;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using CourseConstructors.CourseConstructors.Infrastructure.Persistance;
using CourseConstructors.CourseConstructors.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseConstructors.CourseConstructors.Infrastructure;

public static class ServiceExtension
{
    public static IServiceCollection ConfigurePersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IContext, Context>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(action=>{
            var connection = configuration.GetConnectionString("Redis");
            action.Configuration = connection;
        });

        return services;
    }
    
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IDistributedCacheService, DistributedCacheService>();
        services.AddScoped<ICourseRepositoryService, CourseRepositoryService>();

        return services;
    }
}