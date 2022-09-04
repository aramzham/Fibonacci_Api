using System.Diagnostics;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Infrastructure.Services;

public class MemoryChecker : IMemoryChecker
{
    public double GetMemory()
    {
        using var proc = Process.GetCurrentProcess();
        // The proc.PrivateMemorySize64 will returns the private memory usage in byte.
        // Would like to Convert it to Megabyte? divide it by 2^20
        return proc.PrivateMemorySize64 / (1024 * 1024);
    }

    public bool IsThresholdReached(double maxMemory) => GetMemory() >= maxMemory;
}