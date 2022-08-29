using FibonacciApi.Api.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FibonacciApi.Api.Infrastructure.Services;

public class CacheManager : ICacheManager
{
    private const string CacheKey = "sequence";
    private readonly IMemoryCache _memoryCache;

    public CacheManager(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    
    public async Task<IEnumerable<int>> Get(int firstIndex, int lastIndex)
    {
        var sequence = await _memoryCache.GetOrCreateAsync(CacheKey, _ => Task.FromResult(new List<int>()));
        return sequence.Skip(firstIndex).Take(lastIndex - firstIndex + 1);
    }
}