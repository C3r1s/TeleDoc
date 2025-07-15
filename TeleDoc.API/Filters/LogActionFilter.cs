using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TeleDoc.API.Filters;

public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation($"Начато выполнение действия: {context.ActionDescriptor.DisplayName}");
        _logger.LogInformation($"Параметры: {string.Join(", ", context.ActionArguments)}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation($"Завершено выполнение действия: {context.ActionDescriptor.DisplayName}");
    }
}

public class LogActionAttribute : TypeFilterAttribute
{
    public LogActionAttribute() : base(typeof(LogActionFilter)) { }
}