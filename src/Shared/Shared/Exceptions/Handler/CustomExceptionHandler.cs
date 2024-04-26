using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shared.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError($"Error message: {exception.Message}, Occurred: {DateTime.UtcNow}");

        (string Detail, string Title, int StatusCode) details = (exception.Message, exception.GetType().Name, 0);

        details.StatusCode = exception switch
        {
            InternalServerException => context.Response.StatusCode = StatusCodes.Status500InternalServerError,
            ValidationException => context.Response.StatusCode = StatusCodes.Status400BadRequest,
            BadRequestException => context.Response.StatusCode = StatusCodes.Status400BadRequest,
            NotFoundException => context.Response.StatusCode = StatusCodes.Status404NotFound,
            _ => context.Response.StatusCode = StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("TraceId", context.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}
