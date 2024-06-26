using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.Common.Behaivors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Before execution for {typeof(TRequest).Name}");

            return await next();
        }
        finally
        {
            _logger.LogError($"After execution for {typeof(TRequest).Name}");
        }
    }
}