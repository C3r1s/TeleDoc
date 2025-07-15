using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TeleDoc.API.Exceptions;

namespace TeleDoc.API.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        
        _logger.LogError(exception, "Произошла ошибка при обработке запроса");

        var problemDetails = new ProblemDetails
        {
            Title = "Ошибка сервера",
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.Message
        };

        switch (exception)
        {
            case ValidationException validationException:
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Ошибка валидации";
                problemDetails.Extensions["errors"] = validationException.Errors;
                break;
                
            case NotFoundException:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Ресурс не найден";
                break;
        }

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };

        context.ExceptionHandled = true;
    }
}

