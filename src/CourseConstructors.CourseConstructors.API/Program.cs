using System.Globalization;
using System.Reflection;
using CourseConstructors.CourseConstructors.API;
using CourseConstructors.CourseConstructors.Core;
using CourseConstructors.CourseConstructors.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// configure API layer
builder.Services.ConfigureVersioning()
    .ConfigureSwaggerGen();

// configure Core layer
builder.Services.AddScopedServices()
    .ConfigureApplicationAssemblies()
    .ConfigureApplicationServices();

// configure Infrastructure layer
builder.Services.ConfigurePersistance(builder.Configuration);
builder.Services.AddStackExchangeRedisCache(action=>{
    var connection = "localhost:6379";
    action.Configuration = connection;
});

var app = builder.Build();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    ApplyCurrentCultureToResponseHeaders = true
});

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
app.ApplyMiddlewares();
    
app.Run();
