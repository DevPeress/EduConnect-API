using EduConnect.Infra.CrossCutting.Entities;
using System.Net;
using System.Net.Mime;

namespace EduConnect.MiddleWares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsJsonAsync(new CustomProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = $@"{exception.Message}. {exception?.InnerException?.Message}",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Instance = context.Request.Path
        });
    }
}
