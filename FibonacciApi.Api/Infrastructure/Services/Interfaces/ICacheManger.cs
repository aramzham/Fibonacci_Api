namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface ICacheManger
{
    bool Contains(int i);
    ulong Get(int n);
    void Set(ulong value, int index);
}