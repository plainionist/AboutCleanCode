using MediatR;

namespace WeatherApp.Infrastructure;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> myLogger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        myLogger = logger;
    }
    
     public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        myLogger.LogInformation($"Processing '{typeof(TRequest).Name}'");

        var response = await next();

        myLogger.LogInformation($"Processed '{typeof(TResponse).Name}'");

        return response;
    }
}