namespace FibonacciApi.Api.Infrastructure.Models;

public class ResponseModel
{
    public IEnumerable<ulong> Sequence { get; set; }
    public string? Message { get; set; }
}