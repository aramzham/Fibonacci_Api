namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface IMemoryChecker
{
    double GetMemory();
    bool IsThresholdReached(double maxMemory);
}