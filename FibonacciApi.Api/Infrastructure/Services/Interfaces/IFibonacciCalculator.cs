namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface IFibonacciCalculator
{
    IEnumerable<int> GetSubsequence(int firstIndex, int lastIndex);
}