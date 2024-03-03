using CourseConstructors.CourseConstructors.Core.Interfaces.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace CourseConstructors.CourseConstructors.Core;

public static class ServiceExtension
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}