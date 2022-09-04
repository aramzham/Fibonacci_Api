namespace FibonacciApi.Api.Infrastructure.Models;

public class ResponseModel
{
    public IEnumerable<int> Sequence { get; set; }
    public string Message { get; set; }
}