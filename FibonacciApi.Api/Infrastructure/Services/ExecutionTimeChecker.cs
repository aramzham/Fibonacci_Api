using System.Diagnostics;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Infrastructure.Services;

public class ExecutionTimeChecker : IExecutionTimeChecker
{
    private readonly Stopwatch _sw;

    public ExecutionTimeChecker() => _sw = new Stopwatch();
    
    public void Run() => _sw.Start();

    public bool IsTimeElapsed(int timeToRun) => _sw.ElapsedMilliseconds >= timeToRun;
}