namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface ICacheManager
{
    Task<IEnumerable<int>> Get(int firstIndex, int lastIndex);
}