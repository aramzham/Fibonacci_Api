using System.Timers;
using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Infrastructure.Services;

public class CacheManager : ICacheManger, IDisposable
{
    private BackgroundTask _task;
    
    private readonly Dictionary<int, ulong> _cache = new() { { 0, 0 }, { 1, 1 } };

    public CacheManager(IConfiguration configuration)
    {
        _task = new BackgroundTask(TimeSpan.FromMilliseconds(int.TryParse(configuration["CacheLifeTimeInMs"], out var clt) ? clt : 60000));
        _task.Start(OnTimerElapsed);
    }
    
    public bool Contains(int i) => _cache.ContainsKey(i);
    
    public ulong Get(int n) => _cache[n];

    public void Set(ulong value, int index) => _cache[index] = value;
    
    private void OnTimerElapsed()
    {
        _cache.Clear();
        _cache.Add(0, 0);
        _cache.Add(1, 1);
    }

    public void Dispose()
    {
        _task.Stop();
    }
}