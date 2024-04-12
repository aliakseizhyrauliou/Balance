using System.Diagnostics;
using Barion.Balance.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barion.Balance.Application.Common.Behaviors;

public class PerformanceBehaviour<TRequest, TResponse>(
    ILogger<TRequest> logger,
    IUser user) 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly Stopwatch _timer = new();

    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= 500)
            return response;
        
        
        var requestName = typeof(TRequest).Name;
        var userId = user.Id ?? string.Empty;
            
        logger.LogWarning("Barion.Balance.Api Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
            requestName, elapsedMilliseconds, userId, request);

        return response;
    }
}