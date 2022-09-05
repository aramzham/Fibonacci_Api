using FibonacciApi.Api.Infrastructure.Models;

namespace FibonacciApi.Api.Infrastructure.Services.Interfaces;

public interface IFibonacciService
{
    ValueTask<ResponseModel> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun,
        double maxMemory);
}