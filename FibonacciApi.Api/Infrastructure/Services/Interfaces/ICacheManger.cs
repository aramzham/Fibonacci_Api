namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface ICacheManger
{
    bool Contains(int i);
    int Get(int n);
    void Set(int value, int index);
}