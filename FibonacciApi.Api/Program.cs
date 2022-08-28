using FibonacciApi.Api.Infrastructure.Middlewares;
using FibonacciApi.Api.Infrastructure.Services;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IFibonacciCalculator, FibonacciCalculatorIterative>();
builder.Services.AddTransient<IFibonacciService, FibonacciService>();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGet(
    "/fib",
    ([FromServices]IFibonacciService fibonacciService, HttpRequest request) =>
        fibonacciService.GetSubsequence(
            int.TryParse(request.Query["firstIndex"], out var fi) ? fi : 0,
            int.TryParse(request.Query["lastIndex"], out var li) ? li : 0,
            bool.TryParse(request.Query["useCache"], out var uc) && uc,
            int.TryParse(request.Query["timeToRun"], out var ttr) ? ttr : 1 * 60 * 1000, // 1 minute
            int.TryParse(request.Query["maxMemory"], out var mm) ? mm : 20 * 1000 // 20mb
            ));

app.Run();