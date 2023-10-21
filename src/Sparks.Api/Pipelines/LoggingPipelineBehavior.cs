using System.Diagnostics;
using MediatR;

namespace Sparks.Api.Pipelines;

public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly Stopwatch _stopwatch;
    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
        _stopwatch = new Stopwatch();
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _stopwatch.Start();
        _logger.LogInformation("Starting request [{Request}] - {StartTime}ms", typeof(TRequest).Name, _stopwatch.ElapsedMilliseconds);
        TResponse response = await next();
        _stopwatch.Stop();
        _logger.LogInformation("Done processing request [{Request}] - {EndTime}ms", typeof(TRequest).Name, _stopwatch.ElapsedMilliseconds);
        return response;
    }
}