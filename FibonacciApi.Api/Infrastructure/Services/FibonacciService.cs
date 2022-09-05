using FibonacciApi.Api.Infrastructure.Models;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Infrastructure.Services;

public class FibonacciService : IFibonacciService
{
    private readonly IMemoryChecker _memoryChecker;
    private readonly IExecutionTimeChecker _timeChecker;
    private readonly ICacheManger _cacheManager;

    public FibonacciService(IMemoryChecker memoryChecker, IExecutionTimeChecker timeChecker, ICacheManger cacheManager)
    {
        _memoryChecker = memoryChecker;
        _timeChecker = timeChecker;
        _cacheManager = cacheManager;
    }

    public async ValueTask<ResponseModel> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun,
        double maxMemory)
    {
        // run the timer
        _timeChecker.Run();

        if (firstIndex < 0 || lastIndex < 0)
            throw new Exception("indexes cannot be negative");

        if (firstIndex > lastIndex)
            throw new Exception("first index cannot be grater than last index");

        var sequence = new List<ulong>();
        var message = default(string);
        for (var i = 0; i <= lastIndex; i++)
        {
            if (_memoryChecker.IsThresholdReached(maxMemory))
            {
                message = $"We have reached the memory threshold of {_memoryChecker.GetMemory()}";
                break;
            }

            if (_timeChecker.IsTimeElapsed(timeToRun))
            {
                message = "Time has elapsed";
                break;
            }

            if (i >= firstIndex)
                sequence.Add(await Fib(i, useCache));
        }

        return new ResponseModel()
        {
            Sequence = sequence.Any()
                ? sequence
                : throw new Exception($"No elements were generated{Environment.NewLine}Reason: {message}"),
            Message = message
        };
    }

    private async Task<ulong> Fib(int n, bool useCache)
    {
        if (useCache && _cacheManager.Contains(n))
            return _cacheManager.Get(n);
        else if (n < 2)
            return (ulong)n;

        var previous = Task.Run(() => Fib(n - 2, useCache));
        var current = Task.Run(() => Fib(n - 1, useCache));

        var value = await previous + await current;

        _cacheManager.Set(value, n);

        return value;
    }
}