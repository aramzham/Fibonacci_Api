using FibonacciApi.Api.Infrastructure.Services.Interfaces;

namespace FibonacciApi.Api.Infrastructure.Services;

public class FibonacciCalculatorIterative : IFibonacciCalculator
{
    public IEnumerable<int> GetSubsequence(int firstIndex, int lastIndex)
    {
        if (firstIndex < 0 || lastIndex < 0)
            throw new Exception("indexes cannot be negative");
        
        if (firstIndex > lastIndex)
            throw new Exception("first index cannot be grater than last index");

        switch (lastIndex)
        {
            case 0:
                yield return 0;
                break;
            case 1:
                yield return 0;
                yield return 1;
                break;
        }

        int previous = 0, current = 1, next, index = 1;

        while (index < lastIndex)
        {
            next = previous + current;
            previous = current;
            current = next;
            index++;

            if (firstIndex <= index)
                yield return next;
        }
    }
}