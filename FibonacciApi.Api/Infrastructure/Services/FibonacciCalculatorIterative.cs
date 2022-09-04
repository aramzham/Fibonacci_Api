using System.Diagnostics;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Infrastructure.Services;

public class FibonacciCalculatorIterative : IFibonacciCalculator
{
    private int _timeoutTimeInMs;

    public FibonacciCalculatorIterative(IConfiguration configuration)
    {
        _timeoutTimeInMs = int.TryParse(configuration["FibMethodExecutionTimeoutInMs"], out var met) ? met : 60000;
    }

    public IEnumerable<int> GetSubsequence(int firstIndex, int lastIndex, ICacheManager cacheManager, int previous = 0,
        int current = 1, int index = 1)
    {
        var sw = new Stopwatch();
        sw.Start();

        if (firstIndex < 0 || lastIndex < 0)
            throw new Exception("indexes cannot be negative");

        if (firstIndex > lastIndex)
            throw new Exception("first index cannot be grater than last index");

        switch (lastIndex)
        {
            case 0:
                yield return 0;
                break;
            case 1:
                yield return 0;
                yield return 1;
                break;
        }

        int next;

        while (index < lastIndex || sw.Elapsed >= TimeSpan.FromMilliseconds(_timeoutTimeInMs))
        {
            next = previous + current;
            previous = current;
            current = next;
            index++;
            cacheManager.Set(next, index);

            if (firstIndex <= index)
                yield return next;
        }
    }
}