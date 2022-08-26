namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface IFibonacciService
{
    Task<IEnumerable<int>> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun, int maxMemory);
}