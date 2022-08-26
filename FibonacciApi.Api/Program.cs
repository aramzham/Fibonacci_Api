using FibonacciApi.Api.Infrastructure.Middlewares;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.MapGet("/fib", (IFibonacciService fibonacciService) => fibonacciService.GetSubsequence());

app.Run();