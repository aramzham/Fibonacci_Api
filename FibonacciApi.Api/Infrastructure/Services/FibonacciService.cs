using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Infrastructure.Services;

public class FibonacciService : IFibonacciService
{
    private readonly IFibonacciCalculator _fibonacciCalculator;

    public FibonacciService(IFibonacciCalculator fibonacciCalculator)
    {
        _fibonacciCalculator = fibonacciCalculator;
    }
    
    public Task<IEnumerable<int>> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun, int maxMemory)
    {
        throw new NotImplementedException();
    }
}