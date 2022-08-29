using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Infrastructure.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibonacciController : ControllerBase
    {
        private readonly IFibonacciService fibonacciService;

        public FibonacciController(IFibonacciService fibonacciService)
        {
            this.fibonacciService = fibonacciService;
        }

        [HttpGet("/fib")]
        public Task<IEnumerable<int>> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun, int maxMemory)
        {
            return fibonacciService.GetSubsequence(firstIndex, lastIndex, useCache, timeToRun, maxMemory);
        }
    }
}
