using CourseConstructors.CourseConstructors.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseConstructors.CourseConstructors.Infrastructure;

public static class ServiceExtension
{
    public static IServiceCollection ConfigurePersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}