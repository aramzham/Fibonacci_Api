using FibonacciApi.Api.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace FibonacciApi.Api.Infrastructure.Services;

public class CacheManager : ICacheManager
{
    private readonly List<int> _cache = new() { 0, 1 };
    
    public int[] Get(int firstIndex, int lastIndex)
    {
        if (firstIndex >= _cache.Count)
            return Array.Empty<int>();
        
        return _cache.Skip(firstIndex).Take(lastIndex - firstIndex + 1).ToArray();
    }

    public void Set(int value, int index)
    {
        if(index == _cache.Count)
            _cache.Add(value);
    }

    public int GetPenultimate() => _cache[^2];

    public int GetUltimate() => _cache[^1];

    public int GetLastIndex() => _cache.Count - 1;
}