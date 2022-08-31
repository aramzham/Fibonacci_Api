namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface ICacheManager
{
    int[] Get(int firstIndex, int lastIndex);
    void Set(int value, int index);
}