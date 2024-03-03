using System.Reflection;
using CourseConstructors.CourseConstructors.API.Middlewares;
using CourseConstructors.CourseConstructors.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CourseConstructors.CourseConstructors.API;

internal static class ServiceExtension
{
    internal static IServiceCollection ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning();

        return services;
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