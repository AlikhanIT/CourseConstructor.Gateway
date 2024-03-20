using System.Globalization;
using CourseConstructors.CourseConstructors.API;
using CourseConstructors.CourseConstructors.Core;
using CourseConstructors.CourseConstructors.Infrastructure;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// configure API layer
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.ConfigureVersioning()
    .ConfigureSwaggerGen();

// configure Core layer
builder.Services.AddScopedServices()
    .ConfigureApplicationAssemblies()
    .ConfigureApplicationServices()
    .ConfigureOptions(builder.Configuration);

// configure Infrastructure layer
builder.Services.ConfigurePersistance(builder.Configuration)
    .ConfigureCaching(builder.Configuration)
    .ConfigureServices();

var defaultCulture = new RequestCulture("ru-RU", "ru-RU");
var supportedCultures = new List<CultureInfo> { new("ru-RU"), new("kk-KZ"), };

var localizationOptions =
    new RequestLocalizationOptions
    {
        DefaultRequestCulture = defaultCulture,
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures,
        ApplyCurrentCultureToResponseHeaders = true,
    };

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseRequestLocalization(localizationOptions);
app.ApplyMiddlewares();
    
app.Run();
