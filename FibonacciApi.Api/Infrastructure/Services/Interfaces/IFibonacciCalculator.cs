namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface IFibonacciCalculator
{
    IEnumerable<int> GetSubsequence(int firstIndex, int lastIndex, ICacheManager cacheManager, int previous = 0, int current = 1, int index = 1);
}