using System.Globalization;
using System.Reflection;
using CourseConstructors.CourseConstructors.Core.Interfaces.Providers;
using CourseConstructors.CourseConstructors.Core.Resources;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
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
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddLocalizer();

        return services;
    }


    private static IServiceCollection AddLocalizer(this IServiceCollection services)
    {
        services.AddLocalization();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("kk"),
                new CultureInfo("ru")
            };

            options.DefaultRequestCulture = new RequestCulture("kk");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        return services;
    }

    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IResourceRetriever, ResourceRetriever>();

        return services;
    }
}