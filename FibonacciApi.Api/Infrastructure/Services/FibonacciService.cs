using FibonacciApi.Api.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FibonacciApi.Api.Infrastructure.Services;

public class FibonacciService : IFibonacciService
{
    private readonly IFibonacciCalculator _fibonacciCalculator;
    private readonly ICacheManager _cacheManager;

    public FibonacciService(IFibonacciCalculator fibonacciCalculator, ICacheManager cacheManager)
    {
        _fibonacciCalculator = fibonacciCalculator;
        _cacheManager = cacheManager;
    }

    public async ValueTask<IEnumerable<int>> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun,
        int maxMemory)
    {
        var partFromCache = Array.Empty<int>();
        if (useCache)
        {
            partFromCache = _cacheManager.Get(firstIndex, lastIndex);
        }

        if (partFromCache.Length == lastIndex - firstIndex + 1)
            return partFromCache;

        var subsequence =
            _fibonacciCalculator.GetSubsequence(
                firstIndex + partFromCache.Length,
                lastIndex,
                _cacheManager,
                _cacheManager.GetPenultimate(),
                _cacheManager.GetUltimate(),
                partFromCache.Any() ? _cacheManager.GetLastIndex() : 1);
        return partFromCache.Any() ? partFromCache.Concat(subsequence) : subsequence;
    }
}