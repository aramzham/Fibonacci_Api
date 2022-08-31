namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface IFibonacciService
{
    ValueTask<IEnumerable<int>> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun, int maxMemory);
}