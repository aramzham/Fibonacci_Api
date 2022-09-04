using FibonacciApi.Api.Infrastructure.Exceptions;
using FibonacciApi.Api.Infrastructure.Models;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Infrastructure.Services;

public class FibonacciCalculatorRecursive
{
    private static readonly Dictionary<int, int> _memo = new() { { 0, 0 }, { 1, 1 } };
    
    private readonly IMemoryChecker _memoryChecker;
    private readonly IExecutionTimeChecker _timeChecker;

    public FibonacciCalculatorRecursive(IMemoryChecker memoryChecker, IExecutionTimeChecker timeChecker)
    {
        _memoryChecker = memoryChecker;
        _timeChecker = timeChecker;
    }

    public async Task<ResponseModel> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun, double maxMemory)
    {
        // run the timer
        _timeChecker.Run();
        
        if (firstIndex < 0 || lastIndex < 0)
            throw new Exception("indexes cannot be negative");

        if (firstIndex > lastIndex)
            throw new Exception("first index cannot be grater than last index");

        var sequence = new List<int>();
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
            Sequence = sequence.Any() ? sequence : throw new Exception("no elements were generated"),
            Message = message
        };
    }

    private async Task<int> Fib(int n, bool useCache)
    {
        if (useCache && _memo.ContainsKey(n))
            return _memo[n];
        else if (n < 2)
            return n;

        var previous = Task.Run(() => Fib(n - 2, useCache));
        var current = Task.Run(() => Fib(n - 1, useCache));

        var value = await previous + await current;

        _memo[n] = value;

        return value;
    }
}