namespace FibonacciApi.Api.Infrastructure.Exceptions;

[Serializable]
public class MemoryThresholdReachedException : Exception
{
    public MemoryThresholdReachedException()
    {
    }

    public MemoryThresholdReachedException(string message)
        : base(message)
    {
    }

    public MemoryThresholdReachedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}