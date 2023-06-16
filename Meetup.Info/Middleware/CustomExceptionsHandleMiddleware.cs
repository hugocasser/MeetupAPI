using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Meetup.Info.Application.Commons.Exceptions;

namespace Meetup.Info.Middleware;

public class CustomExceptionsHandleMiddleware
{
    public readonly RequestDelegate _next;

    public CustomExceptionsHandleMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    public  Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (ex)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.ValidationResult);
                break;
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                break;
            case EventRecentlyAddedException eventRecentlyAddedException:
                code = HttpStatusCode.PreconditionFailed;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { errpr = ex.Message });
        }

        return context.Response.WriteAsync(result);
    }
}