using FibonacciApi.Api.Infrastructure.Middlewares;
using FibonacciApi.Api.Infrastructure.Services;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICacheManger, CacheManager>();
builder.Services.AddTransient<IFibonacciService, FibonacciService>();
builder.Services.AddTransient<IMemoryChecker, MemoryChecker>();
builder.Services.AddTransient<IExecutionTimeChecker, ExecutionTimeChecker>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGet(
    "/fib",
    ([FromServices] IFibonacciService fibonacciService, int? firstIndex, int? lastIndex, bool? useCache, int? timeToRun,
            double? maxMemory) =>
        fibonacciService.GetSubsequence(
            firstIndex ?? 0,
            lastIndex ?? 0,
            useCache ?? false,
            timeToRun ?? 1 * 60 * 1000, // 1 minute
            maxMemory ?? 100 // 100mb
        ));

app.Run();