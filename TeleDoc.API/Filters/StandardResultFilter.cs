using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TeleDoc.API.Filters;

public class StandardResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            context.Result = new OkObjectResult(new
            {
                Success = true,
                Data = objectResult.Value,
                Timestamp = DateTime.UtcNow
            });
        }
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}