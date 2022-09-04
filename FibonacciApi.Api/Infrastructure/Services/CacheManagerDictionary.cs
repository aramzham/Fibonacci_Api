using System.Timers;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Infrastructure.Services;

public class CacheManagerDictionary
{
    private System.Timers.Timer _cacheInvalidationTimer;
    
    private readonly Dictionary<int, int> _cache = new() { { 0, 0 }, { 1, 1 } };

    public CacheManagerDictionary(IConfiguration configuration)
    {
        _cacheInvalidationTimer = new System.Timers.Timer();
        _cacheInvalidationTimer.Elapsed += OnTimerElapsed;
        _cacheInvalidationTimer.Interval =
            int.TryParse(configuration["CacheLifeTimeInMs"], out var clt) ? clt : 60000;
        _cacheInvalidationTimer.Enabled = true;
    }
    
    public int Get(int n) => _cache[n];

    public void Set(int value, int index) => _cache[index] = value;
    
    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _cache.Clear();
        _cache.Add(0, 0);
        _cache.Add(1, 1);
    }
}