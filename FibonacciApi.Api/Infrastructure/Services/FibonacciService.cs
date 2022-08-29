using FibonacciApi.Api.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FibonacciApi.Api.Infrastructure.Services;

public class FibonacciService : IFibonacciService
{
    private readonly IFibonacciCalculator _fibonacciCalculator;
    private readonly IMemoryCache _memoryCache;

    public FibonacciService(IFibonacciCalculator fibonacciCalculator, IMemoryCache memoryCache)
    {
        _fibonacciCalculator = fibonacciCalculator;
        _memoryCache = memoryCache;
    }
    
    public async Task<IEnumerable<int>> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun, int maxMemory)
    {
        if (useCache)
        {
            var sequenceFromCache = await _memoryCache.GetOrCreateAsync("sequence", _ => Task.FromResult(new List<int>()));
        }

        var subsequence = _fibonacciCalculator.GetSubsequence(firstIndex, lastIndex);
        return subsequence;
    }
}