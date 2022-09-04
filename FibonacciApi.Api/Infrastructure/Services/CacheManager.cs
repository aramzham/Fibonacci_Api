using System.Timers;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Timer = System.Timers.Timer;

namespace FibonacciApi.Api.Infrastructure.Services;

public class CacheManager : ICacheManager
{
    private static Timer _cacheInvalidationTimer;

    private readonly List<int> _cache = new() { 0, 1 };

    public CacheManager(IConfiguration configuration)
    {
        if (_cacheInvalidationTimer is null)
        {
            _cacheInvalidationTimer = new Timer();
            _cacheInvalidationTimer.Elapsed += OnTimerElapsed;
            _cacheInvalidationTimer.Interval =
                int.TryParse(configuration["CacheLifeTimeInMs"], out var clt) ? clt : 60000;
            _cacheInvalidationTimer.Enabled = true;
        }
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _cache.Clear();
        _cache.Add(0);
        _cache.Add(1);
    }

    public int[] Get(int firstIndex, int lastIndex)
    {
        if (firstIndex >= _cache.Count)
            return Array.Empty<int>();

        return _cache.Skip(firstIndex).Take(lastIndex - firstIndex + 1).ToArray();
    }

    public void Set(int value, int index)
    {
        if (index == _cache.Count)
            _cache.Add(value);
    }

    public int GetPenultimate() => _cache[^2];

    public int GetUltimate() => _cache[^1];

    public int GetLastIndex() => _cache.Count - 1;
}