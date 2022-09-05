namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface ICacheManger
{
    bool Contains(ulong i);
    ulong Get(ulong n);
    void Set(ulong value, ulong index);
}