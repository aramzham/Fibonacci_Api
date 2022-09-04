using FibonacciApi.Api.Infrastructure.Middlewares;
using FibonacciApi.Api.Infrastructure.Services;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IFibonacciCalculator, FibonacciCalculatorIterative>();
builder.Services.AddTransient<IFibonacciService, FibonacciService>();
builder.Services.AddSingleton<ICacheManager, CacheManager>();
builder.Services.AddSingleton<CacheManagerDictionary>();
builder.Services.AddTransient<FibonacciCalculatorRecursive>();
builder.Services.AddTransient<IMemoryChecker, MemoryChecker>();
builder.Services.AddTransient<IExecutionTimeChecker, ExecutionTimeChecker>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

// app.MapGet(
//     "/fib",
//     ([FromServices]IFibonacciService fibonacciService, HttpRequest request) =>
//         fibonacciService.GetSubsequence(
//             int.TryParse(request.Query["firstIndex"], out var fi) ? fi : 0,
//             int.TryParse(request.Query["lastIndex"], out var li) ? li : 0,
//             bool.TryParse(request.Query["useCache"], out var uc) && uc,
//             int.TryParse(request.Query["timeToRun"], out var ttr) ? ttr : 1 * 60 * 1000, // 1 minute
//             int.TryParse(request.Query["maxMemory"], out var mm) ? mm : 20 // 20mb
//             ));

app.MapGet(
    "/fib",
    ([FromServices]FibonacciCalculatorRecursive fibonacciService, HttpRequest request) =>
        fibonacciService.GetSubsequence(
            int.TryParse(request.Query["firstIndex"], out var fi) ? fi : 0,
            int.TryParse(request.Query["lastIndex"], out var li) ? li : 0,
            bool.TryParse(request.Query["useCache"], out var uc) && uc,
            int.TryParse(request.Query["timeToRun"], out var ttr) ? ttr : 1 * 60 * 1000, // 1 minute
            double.TryParse(request.Query["maxMemory"], out var mm) ? mm : 100 // 100mb
        ));

app.Run();