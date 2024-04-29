using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling request {typeof(TRequest).Name} with response {typeof(TResponse).Name} and data {request}");

        var timer = Stopwatch.StartNew();

        var response = await next();

        timer.Stop();
        if (timer.Elapsed.Seconds > 0)
        {
            logger.LogWarning($"The request {typeof(TRequest).Name} took {timer.Elapsed.Seconds} seconds");
        }

        logger.LogInformation($"Handled request {typeof(TRequest).Name} with response {typeof(TResponse).Name} which returned {response}");

        return response;
    }
}
