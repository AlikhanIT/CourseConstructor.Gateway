using System.Reflection;
using CourseConstructors.CourseConstructors.Core.Interfaces.Providers;
using CourseConstructors.CourseConstructors.Core.Options;
using CourseConstructors.CourseConstructors.Core.Providers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseConstructors.CourseConstructors.Core;

public static class ServiceExtension
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection ConfigureApplicationAssemblies(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddLocalizer();
        
        return services;
    }
    
    private static IServiceCollection AddLocalizer(this IServiceCollection services)
    {
        services.AddLocalization(o => { o.ResourcesPath = "Resources"; });

        services.AddControllers()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
            });

        return services;
    }

    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IResourceRetriever, ResourceRetriever>();

        return services;
    }
    
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TestOptions>(configuration.GetSection(TestOptions.SectionName));

        return services;
    }
}