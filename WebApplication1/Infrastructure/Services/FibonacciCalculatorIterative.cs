using WebApplication1.Infrastructure.Services.Interfaces;

namespace WebApplication1.Api.Infrastructure.Services
{

    public class FibonacciCalculatorIterative : IFibonacciCalculator
    {
        public int GetNth(int n)
        {
            var fibs = new int[n + 1];
            if (n == 0)
                return 0;
            fibs[0] = 0;
            if (n == 1)
                return 1;
            fibs[1] = 1;
            for (var i = 2; i <= n; i++)
            {
                fibs[i] = fibs[i - 2] + fibs[i - 1];
            }
            return fibs[n];
        }
    }
}