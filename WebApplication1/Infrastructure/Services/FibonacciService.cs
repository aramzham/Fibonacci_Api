using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Infrastructure.Services.Interfaces;

namespace WebApplication1.Api.Infrastructure.Services
{
    public class FibonacciService : IFibonacciService
    {
        private readonly IFibonacciCalculator _fibonacciCalculator;

        public FibonacciService(IFibonacciCalculator fibonacciCalculator)
        {
            _fibonacciCalculator = fibonacciCalculator;
        }

        public Task<IEnumerable<int>> GetSubsequence(int firstIndex, int lastIndex, bool useCache, int timeToRun, int maxMemory)
        {
            return Task.FromResult(FibonacciCalculator(firstIndex, lastIndex, timeToRun));

        }

        public IEnumerable<int> FibonacciCalculator(int first, int last, int time)
        {

            List<int> temp = new List<int>();

            List<int> result = new List<int>();

            temp.Add(0);
            temp.Add(1);

            int previos = 0;
            int current = 1;
            int next = current + previos;
            int nextIndex = 2;
            bool breakOperation = false;
            Stopwatch StopWatch = new Stopwatch();

            if (time != 0)
            {

                StopWatch.Start();
            }


            if (first > last)
            {
                throw new Exception("Not valid");
            }

            if (last == 0)
            {
                yield return 0;
            }

            if (first == 0 && last == 1)
            {
                yield return 0;
                yield return 1;
            }

            //Milisecunds
            //2022 - 08 - 30 02:13:29.2124032
            //2022 - 08 - 30 02:13:29.2125146
            //2022 - 08 - 30 02:13:29.2125998
            //2022 - 08 - 30 02:13:29.2126677
            //2022 - 08 - 30 02:13:29.2127438
            //2022 - 08 - 30 02:13:29.2136498
            //2022 - 08 - 30 02:13:29.2141811
            while (nextIndex < last || breakOperation)
            {
                if (StopWatch.ElapsedMilliseconds >= time && time != 0)
                {
                    StopWatch.Stop();
                    breakOperation = true;
                    break;
                }
                previos = current;
                current = next;
                next = current + previos;
                nextIndex++;

                if (first <= nextIndex)
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    yield return next;
                }
            }
        }
    }
}