namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface IExecutionTimeChecker
{
    void Run();
    bool IsTimeElapsed(long timeToRun);
}