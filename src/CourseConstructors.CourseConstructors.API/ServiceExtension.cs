using System.Reflection;
using CourseConstructors.CourseConstructors.API.Middlewares;
using CourseConstructors.CourseConstructors.Core.Common.Behaivors;
using MediatR;
using Serilog;

namespace CourseConstructors.CourseConstructors.API;

internal static class ServiceExtension
{
    internal static IServiceCollection ConfigureVersioning(this IServiceCollection services)
    {
        return services
            .AddApiVersioning();
    }
    internal static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(typeof(Program))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }
    
    internal static void ConfigureSerilog(this WebApplicationBuilder host, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        host.Logging.ClearProviders();
        host.Logging.AddSerilog();
    }
    
    internal static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
        
        return services;
    }
    
    internal static WebApplication ApplyMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}