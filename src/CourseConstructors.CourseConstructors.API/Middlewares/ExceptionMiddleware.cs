using CourseConstructors.CourseConstructors.Core.Common.SharedResponses;
using Newtonsoft.Json;

namespace CourseConstructors.CourseConstructors.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;  
    public ExceptionMiddleware(RequestDelegate next)  
    {  
        _next = next ?? throw new ArgumentNullException(nameof(next));  
    }  
    public async Task InvokeAsync(HttpContext context)  
    {
        try
        {
            await this._next(context);  
        }
        catch (Exception e)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorModel = new ErrorResponse(context.Response.StatusCode, e.Message);
            string jsonResponse = JsonConvert.SerializeObject(errorModel);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}