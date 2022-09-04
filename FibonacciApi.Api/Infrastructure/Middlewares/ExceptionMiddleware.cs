using System.Net;
using FibonacciApi.Api.Infrastructure.Models;

namespace FibonacciApi.Api.Infrastructure.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex.Message);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, string? message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new ErrorModel()
        {
            StatusCode = context.Response.StatusCode,
            Message = message ?? "Internal Server Error from the custom middleware."
        }.ToString());
    }
}