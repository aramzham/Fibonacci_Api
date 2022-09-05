using System.ComponentModel.DataAnnotations;

namespace FibonacciApi.Api.Tests;

public class ExecutionTimeCheckerTests
{
    private readonly ExecutionTimeChecker _sut = new();

    [Theory, AutoData]
    public async Task IsTimeElapsed_WhenTimeElapsed_ReturnsTrue(int timeToRun)
    {
        // arrange
        _sut.Run();

        // act
        await Task.Delay(timeToRun + 100);

        // assert
        _sut.IsTimeElapsed(timeToRun).Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task IsTimeElapsed_WhenTimeNotElapsed_ReturnsFalse([Range(1000, 2000)] int timeToRun)
    {
        // arrange
        _sut.Run();

        // act
        await Task.Delay(timeToRun - 500);

        // assert
        _sut.IsTimeElapsed(timeToRun).Should().BeFalse();
    }
}