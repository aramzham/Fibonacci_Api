using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Infrastructure.Services.Interfaces
{

    public interface IFibonacciService
    {
        Task<IEnumerable<int>> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun, int maxMemory);
    }
}